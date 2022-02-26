INSERT INTO `RESEARCH_GROUP` (`Name`, `Description`) VALUES 
('Веселые Звездочки', 'В нашей группе нет места грусти'), 
('Красные карлики', 'По названию все понятно'),
('Газовый гигант', 'Мы состоим из водорода и гелия'),
('Анонимы', NULL),
('Черная дыра', 'Мы не хотим туда');

INSERT INTO `USER_TYPE` (`Name`, `Description`) VALUES 
('Администратор', 'Пользователь с полными правами'),
('Ученый', 'Пользователь с ограниченными правами'),
('Обычеый аккаунт', 'Только для чтения'),
('Аноним', NULL);

INSERT INTO `USER` 
(`Name`, `Email`, `PasswordHach`, `TupeUser`, `ResearchGroup`) VALUES
('Максим', 'Max312@gmail.com', '672131', '1', '2'),
('Сёма', 'lolmail@mail.com', '123123', '4', '4'),
('User13', '13User13@gmail.com', '341234', '4', '2'),
('Вася', 'qwerty@outlook.com', '213412', '3', NULL),
('Игнат', 'IgnatMail@gmail.com', '112233', '2', '1'),
('Сом', 'SOM3000@mail.com', '672131', '3', NULL);

INSERT INTO `EXOPLANET_DETECTION_METHOD`
(`Name`, `Description`) VALUES
('Метод Доплера', 'Вращающиеся планеты заставляют звезды колебаться в пространстве, изменяя цвет света, который наблюдают астрономы'),
('Транзитный метод', 'Когда планета проходит непосредственно между своей звездой и наблюдателем, она ослабляет свет звезды на измеримую величину'),
('Прямое наблюдение', 'Астрономы могут фотографировать экзопланеты, удаляя подавляющее сияние звезд, вокруг которых они вращаются'),
('Гравитационное микролинзирование', 'Свет от далекой звезды изгибается и фокусируется под действием силы тяжести, когда планета проходит между звездой и Землей'),
('Астрометрия', 'Орбита планеты может привести к тому, что звезда будет колебаться в пространстве по отношению к близлежащим звездам на небе');

INSERT INTO `EXOPLANET_TYPE`
(`Name`, `Description`) VALUES
('Горячий юпитер', 'Газовый гигант (не меньше Юпитера), находящийся значительно ближе к своей звезде, чем Меркурий к Солнцу.'),
('Пульсарная планета', 'Пульсарной планетой именуют экзопленту, которая обращается вокруг пульсара. Как вы знаете, пульсар это не простая звезда, а очень плотный и быстро вращающийся остаток сверхновой.'),
('Суперземля', 'Суперземлей именуются экзопланеты, которые имеют массу больше нашей Земли, но существенно уступают таким гигантам, как Уран или Нептун.'),
('Горячие нептуны', 'Эти объекты весьма похожи на горячий Юпитер, но существенно уступают по массе. Газовые гиганты расположены очень близко к родительской звезде и примерно в 15-20 раз тяжелее Земли.'),
('Планета-океан', 'Такая экзотическая экзопланета покрыта водой и не имеет участков суши.'),
('Хтоническая планета', 'Когда горячий Юпитер расположен слишком близко к звезде, то планета рискует утратить всю свою газовую оболочку и тогда останется лишь небольшое каменное или металлическое ядро.'),
('Планета-сирота', 'Эти загадочные экзопланеты путешествуют по-космическому пространству в гордом одиночестве без какой-либо привязанности к звезде или системе звезд.');

INSERT INTO `DISCOVERER`
(`Name`, `Description`) VALUES
('Веселые Звездочки', 'В нашей группе нет места грусти'), 
('Красные карлики', 'По названию все понятно'),
('Иоганн Кеплер', 'Древний ученый');

INSERT INTO `EXOPLANET` 
(`Name`, `Status`, `DateAdded`, `DateConfirmation`, `Mass`, `Radius`, `OrbitalRadius`, `User`, `DetectionMethod`, `Type`, `Discoverer`) VALUES
('Смеиана', '1', '2013-02-11', '2015-03-07', '5', '2', '4', '5', '3', '5', '1'),
('Григорий', '0', '2022-02-11', NULL, NULL, NULL, '6', '1', '5', NULL, '1'),
('София', '1', '2010-08-23', '2012-05-21', '7', '5', '4', '1', '2', '1', '2'),
('Лев', '1', '2011-03-17', '2011-11-11', '3', '1', '2', '1', '3', '2', '2'),
('Рай', '0', '2000-01-01', NULL, '12', '7', '33', NULL, NULL, '3', '2'),
('АД', '0', '2000-01-01', NULL, '13', '6', '666', NULL, NULL, '6', '1');

INSERT INTO `STAR_TYPE`
(`Name`, `Description`) VALUES
('Жёлтый карлик', 'Тип небольших звёзд главной последовательности, имеющих массу от 0,8 до 1,2 массы Солнца и температуру поверхности 5000–6000 K.'),
('Красный гигант', 'Это крупная звезда красноватого или оранжевого цвета. Образование таких звезд возможно как на стадии звездообразования, так и на поздних стадиях их существования.'),
('Белый карлик', 'Белый карлик – это то, что остаётся от обычной звезды с массой, не превышающей 1,4 солнечной массы, после того, как она проходит стадию красного гиганта.'),
('Красный карлик', 'Красные карлики – самые распространённые объекты звёздного типа во Вселенной. Оценка их численности варьируется в диапазоне от 70 до 90% от числа всех звёзд в галактике. Они довольно сильно отличаются от других звезд.'),
('Коричневый карлик', 'Коричневый карлик – субзвездные объекты, в недрах которых, в отличие от звезд главной последовательности, не происходит реакции термоядерного синтеза c превращением водорода в гелий.'),
('Субкоричневые карлики', 'Субкоричневые карлики или коричневые субкарлики – холодные формирования, по массе лежащие ниже предела коричневых карликов.'),
('Черный карлик', 'Черные карлики – остывшие и вследствие этого не излучающие в видимом диапазоне белые карлики. Представляет собой конечную стадию эволюции белых карликов. Массы черных карликов, подобно массам белых карликов, ограничиваются сверху 1,4 массами Солнца.'),
('Нейтронная звезда', 'Нейтронные звезды (НЗ) – это звездные образования с массами порядка 1,5 солнечных и размерами, заметно меньшими белых карликов, типичный радиус нейтронной звезды составляет, предположительно, порядка 10—20 километров');


INSERT INTO `STAR_DETECTION_METHOD`
(`Name`, `Description`) VALUES
('Транзитный метод', NULL),
('Прямое наблюдение', NULL),
('Гравитационное микролинзирование', NULL);


INSERT INTO `STAR` 
(`Name`, `Status`, `DateAdded`, `DateConfirmation`, `Mass`, `Radius`, `User`, `DetectionMethod`, `Type`, `Discoverer`) VALUES
('Пугачева', '1', '1949-04-15', '1980-06-26', '65', '17', '5', '2', '8', '1'),
('Бетельгейзе', '1', '1900-05-12', '1900-05-12', '6', '3', NULL, '2', '2', '3'),
('Рок Звезда', '1', '1949-04-15', '1980-06-26', '65', '17', '5', '2', '1', '1'),
('Сосиска', '0', '2016-04-15', NULL, '5', '2', '1', '3', '1', '2');

INSERT INTO `STAR_AND_EXOPLANET`
(`Star`, `Exoplane`) VALUES
('1', '4'),
('3', '1'),
('3', '2'),
('4', '5'),
('4', '6'),
('1', '3');