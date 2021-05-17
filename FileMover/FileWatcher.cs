using FileMover.model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
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
            while (Directory.GetFiles(rule.DirStart, rule.FileMask).Except(recheсk.Keys).ToList().Count > 0)//.Where(o => o < 4).Length > 0)
            {
                string f = new DirectoryInfo(rule.DirStart)
                    .GetFiles(rule.FileMask, SearchOption.TopDirectoryOnly)
                    .Where(w => !recheсk.Keys.Contains(w.FullName))
                    .OrderBy(p => p.CreationTime)
                    .First().FullName;

                FileAction(f);
            }

            while (recheсk.Where(o => o.Value < Const.MaxRecheckFile).ToList().Count > 0)
            {
                var s = recheсk.Where(o => o.Value < Const.MaxRecheckFile).First();

                Thread.Sleep((int)Math.Pow((int)s.Value, 3) * 1000);

                if (!File.Exists(s.Key))
                    recheсk.Remove(s.Key);
                else
                {
                    FileAction(s.Key);
                }               
            }

            if (historyContext.history.Item.Count > settingsContext.settings.MaxCountHistory)
            {
                historyContext.history.Item.RemoveAll(x => x.DateMove < settingsContext.settings.LastClearHistory);
                historyContext.EditHistory();
                settingsContext.settings.LastClearHistory = DateTime.Now;
                settingsContext.EditSettings();
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
                File.SetAttributes(file, FileAttributes.Normal);
                using (var fs = File.Open(file, FileMode.Open, FileAccess.ReadWrite, FileShare.None)) { }
            }
            catch (Exception e)
            {
                recheсk[file] = !recheсk.ContainsKey(file) ? 1 : recheсk[file].Value + 1;

                if (recheсk[file].Value >= Const.MaxRecheckFile)
                {
                    res = new Response()
                    {
                        respType = Response.RespType.Error,
                        Message = "Ошибка. " + e.Message
                    };

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
                return;
            }

            try
            {
                if (File.Exists(p) && GetHashString(p) == GetHashString(file))
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

        private string GetHashString(string s)
        {
            using (SHA256 SHA256 = SHA256Managed.Create())
            {
                using (FileStream fileStream = File.OpenRead(s))
                    return Convert.ToBase64String(SHA256.ComputeHash(fileStream));
            }
        }

        public void Dispose()
        {
            watcher.Dispose();
        }
    }
}