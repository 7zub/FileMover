using FileMover.model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static FileMover.context.EnumContext;

namespace FileMover
{
    public class FileWatcher : IDisposable
    {
        public Response res;
        private RuleItem rule;
        private ListHistoryContext historyContext;
        private FileSystemWatcher watcher;
        private ListSettingsContext settingsContext;
        private Panel dg;
        private Dictionary<string, int?> recheсk = new Dictionary<string, int?>();

        public FileWatcher(RuleItem rule, ListHistoryContext historyContext, Panel dg, ListSettingsContext settingsContext)
        {
            try
            {
                this.rule = rule;
                this.historyContext = historyContext;
                this.settingsContext = settingsContext;
                this.dg = dg;
                FWCheck();        // ФЛК правила (мало ли)
                //FWExecuteRule();  // обрабатываем файлы, что уже были до запуска FW
                FWInstance();     // реализация FW для правила

                res = new Response()
                {
                    respType = Response.RespType.Success,
                    Message = "Правило № " + rule.Id + " запущено"
                };
            }
            catch (Exception ex)
            {
                res.respType = Response.RespType.Error;
                res.Message = "Ошибка запуска правила № " + rule.Id + ":\n" + ex.Message;
            }
        }

        private void FWCheck()
        {
        }

        public void FWInstance()
        {
            watcher = new FileSystemWatcher();
            watcher.Path = rule.DirStart;
            watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.Size;
            watcher.Filter = rule.FileMask;
            watcher.Created += FWEvent;
            watcher.Changed += FWEvent;
            watcher.EnableRaisingEvents = true;
        }

        private void FWEvent(object source, FileSystemEventArgs e)
        {
            watcher.EnableRaisingEvents = false;
            FWExecuteRule();
            watcher.EnableRaisingEvents = true;
        }

        public void FWExecuteRule()
        {
            string[] dir = Directory.GetFiles(rule.DirStart, rule.FileMask);

            for (int i = 0; i < dir.Length; i++)
            {
                FileAction(dir[i]);
            }

            //if (recheсk[dir[i]] > 0 && recheсk[dir[i]] < 4)
            //await Task.Run(() => RecheckDir());

            if (historyContext.history.Item.Count > settingsContext.settings.MaxCountHistory)
            {
                historyContext.history.Item.RemoveAll(x => x.DateMove < settingsContext.settings.LastClearHistory);
                historyContext.EditHistory();
                settingsContext.settings.LastClearHistory = DateTime.Now;
                settingsContext.EditSettings();
            }
        }

        private void RecheckDir()
        {
            //while (Directory.GetFiles(rule.DirStart, rule.FileMask).Length > 0)
            foreach (var i in recheсk.Where(o => o.Value < 4))
            {
                Thread.Sleep(3000); //(int)Math.Pow(recheсk, 2) * 1000);
                FileAction(i.Key);
            }
        }

        public void FileAction(string file)
        {
            DateTime timeStart = DateTime.Now;
            int fileSize = (int)new FileInfo(file).Length;
            Response res = new Response();

            string p = rule.DirDest + @"\" + Path.GetFileName(file);
            string gen = GenNewFName(p);

            try
            {
                using (var fs = File.Open(file, FileMode.Open, FileAccess.Read, FileShare.None)) { }
            }
            catch (IOException)
            {
                recheсk[file] = !recheсk.ContainsKey(file) ? 0 : recheсk[file]++;
                return;
            }

            try
            {
                if (File.Exists(p) && GetHashString(File.ReadAllText(p)) == GetHashString(File.ReadAllText(file)))
                {
                    res = new Response()
                    {
                        respType = Response.RespType.Warning,
                        Message = "Хеш-суммы файлов совпадают"
                    };
                }
                else
                {
                    File.Copy(
                        file,
                        rule.IfExist == ifEx.rename ? gen : p,
                        rule.IfExist == ifEx.rewrite
                    );

                    res = new Response()
                    {
                        respType = Response.RespType.Success,
                        Message = "Выполнено"
                    };
                }
                // так или иначе удаляем файл
                if (rule.Operation == op.move)
                {
                    File.SetAttributes(file, FileAttributes.Normal);
                    File.Delete(file);
                }
            }
            catch (Exception e)
            {
                res = new Response()
                {
                    respType = Response.RespType.Error,
                    Message = "Ошибка. " + e.Message
                };
                return;
            }
            finally
            {
                historyContext.history.Item.Add(new HistoryItem()
                {
                    Id = historyContext.history.Item.Count > 0 ? historyContext.history.Item.Max(f => f.Id) + 1 : 1,
                    DateMove = DateTime.Now,
                    Filename = Path.GetFileName(file),
                    DirStart = rule.DirStart + @"\" + Path.GetFileName(file),
                    DirDest = rule.DirDest + @"\" + Path.GetFileName(gen),
                    Duration = (int)(DateTime.Now - timeStart).TotalMilliseconds,
                    FileSize = fileSize,
                    result = res
                });

                historyContext.EditHistory();
                _ = dg.Invoke((MethodInvoker)delegate
                {
                    dg.GridRefresh();
                });
            }
        }

        private string GenNewFName(string file)
        {
            int i = 1;
            string GenName = file;

            while (File.Exists(GenName))
                GenName = Path.GetDirectoryName(file) + @"\" + Path.GetFileNameWithoutExtension(file) + Const.RenameFile + i++ + Path.GetExtension(file);

            return GenName;
        }

        private Guid GetHashString(string s)
        {
            //переводим строку в байт-массим  
            byte[] bytes = Encoding.Unicode.GetBytes(s);
            //создаем объект для получения средст шифрования  
            MD5CryptoServiceProvider CSP = new MD5CryptoServiceProvider();
            //вычисляем хеш-представление в байтах  
            byte[] byteHash = CSP.ComputeHash(bytes);
            string hash = string.Empty;
            //формируем одну цельную строку из массива  
            foreach (byte b in byteHash)
                hash += string.Format("{0:x2}", b);
            return new Guid(hash);
        }

        public void Dispose()
        {
            watcher.Dispose();
        }
    }
}