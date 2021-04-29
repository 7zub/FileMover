# Перемещатор файлов (FileMover)
Программа предназначена для мониторинга папок на появление в них новых файлов и последующего их перемещения по пользовательским правилам.

### Функции программы:
1. Отслеживание содержимого заданных каталогов по указанной маске имени(поддерживается знак * и ?);
3. Копирование/перемещение при появлении файлов в отслеживаемых папках;
4. Настройка действия (замена, перебор свободного имени, игнорирование), если файл с данным именем в папке назначения уже существует;
5. Логирование операций над обнаруженными файлами;
6. Возможность смены кретинского названия программы в настройках.

### Пример добавления правила
![Снимок](https://user-images.githubusercontent.com/41264164/116609696-5ccd7500-a93d-11eb-95b1-a52812004a75.PNG)

### Связанные файлы
1. Rules.json - правила
2. History.json - лог операций над файлами
3. Settings.json - пользовательские настройки


```diff
! Если отсутствует один из файлов, программа создаст их заново
```
