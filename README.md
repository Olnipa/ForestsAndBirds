# Test task for Unfrozen Studio, vacancy **Gameplay Programmer (Unity, C#) **
# (Тестовое задание для Unfrozen Studio, вакансия **Gameplay Programmer (Unity, C#)) **

[Link to task condition (Ссылка на условие задания)](https://unfrozen.notion.site/Gameplay-Programmer-Unity-C-8ec30a4e89a149dabdf40414a58c6ef3)

The project was completed in Unity version 2022.3.4f1 (Проект выполнен в Unity версии 2022.3.4f1)

## Task (Задача)

(Eng)
- Develop a prototype of a global map with plot mission points to which you need to send one or another hero and earn points for them.
- **The simplest gameplay cycle:** Map with missions - see the mission - select the mission - read the pre-history of the mission - select a hero to complete - start the mission - read the contents of the mission - repeat.
- **Add. task:** The prototype must be configured “not by a programmer”, but by editing special. file - indicate the number of points, their location and all their contents.

(Rus)
- Разработать прототип глобальной карты с точками-сюжетными-миссиями, на которые необходимо отправлять того или иного героя и зарабатывать им очки.
- **Простейший геймплейный цикл:** Карта с миссиями — видишь миссию — выбираешь миссию — читаешь пред-историю миссии — выбираешь героя для прохождения — стартуешь миссию — читаешь содержимое миссии — повторить.
- **Доп. задача:** Прототип должен настраиваться “не программистом”, а путем редактирования спец. файла - указываться количество точек, их расположение и все их содержимое.

## Project description (Описание проекта)

### Special file (Спец. файл)

(Eng)
- A file of type **csv** was chosen as a “special file” for setting up the prototype by a “non-programmer”. Unlike, for example, a scriptable object, a **csv** file will allow you to store it online in the future and change the settings specified in it at any time (including in builds) without the need to upload a new build and undergo site moderation.

- The ";" character is selected as the section separator in the csv file. If the data from the file is displayed incorrectly, then in the editor you need to specify the ";" character as a separator. For example, in Excel it is usually: File => Options => Advanced => "Editing Options" block => Section separator.

(Rus)
- В качестве "спец. файла" для настройки прототипа "не программистом" был выбран файл типа **csv**. В отличие от например скриптабл объекта, **csv** файл позволит в перспективе хранить его онлайн и менять указанные в нем настройки когда угодно (в том числе в билдах) без необходимости загружать новый билд и проходить модерацию площадок.

- В качестве разделителя разделов в файле csv выбран символ ";". Если данные из файла отображаются некорректно, значит в редакторе необходимо указать в качестве разделителя символ ";". Например в Excel обычно это: Файл => Параметры => Дополнительно => Блок "Параметры правки" => Разделитель разделов.

### Rules for filling out special file (Правила заполнения спец. файла)

(Eng)
- You can add new mission lines. More detailed explanations for filling out each column are indicated in the header of the table. Please note that blank cells are only allowed in the following columns:
     1. Columns with Points for completing a mission
     2. Mutually exclusive mission ID
     3. Mission IDs to unlock
     4. Character Unlock
- Using the symbol ";" not allowed in the file!
- In the “Mission ID” column you can specify any ID, but for each mission it must be unique
- When filling out the “Mutually Exclusive Mission ID” and “Unlock Mission ID” columns, it is important to indicate only the existing IDs from the first “Mission ID” column.

(Rus)
- Можно добавлять новые строки миссий. Более подробные пояснения по заполнению каждого столбца указаны в шапке таблицы. Обратите внимание, что пустые ячейки допускаются только в следующих столбцах:
    1. Столбцы с Очками за прохождение миссии
    2. ID взаимоисключающей миссии
    3. ID Миссий для разблокировки
    4. Анлок персонажа
- Использование символа ";" в файле не допускается!
- В столбце "ID миссий" можно указывать любые ID, но для каждой миссии оно должно быть уникальным
- При заполнении столбцов "ID взаимоисключающей миссии" и "Анлок Миссий" важно указывать только существующие ID из первого столбца "ID миссии".

### Starting scene (Стартовая сцена)

(Eng)
- The starting scene is a single GameScene. It is also the entry point of the BootStrap project.

(Rus)
- Стартовой сценой является единственная сцена GameScene. На ней же находится точка входа проекта BootStrap.
