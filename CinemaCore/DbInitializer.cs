using System.Collections.Specialized;
using CinemaCore;
using CinemaCore.Models;

namespace WebCinema
{
    public static class DbInitializer
    {
        static Random randObj = new Random(1);

        public static void Initialize(CinemaContext db)
        {
            db.Database.EnsureCreated();

            ///Имена, отчества и фамилии
            string FemaleSuf = "на";
            string MaleSuf = "ич";
            string[] MaleName = { "Даниил", "Данил", "Андрей", "Олег", "Иван", "Евгений", "Артем", "Владислав", "Владимир", "Александр", "Кирилл", "Павел", "Артемий", "Василий", "Максим", "Дмитрий", "Алексей" };
            string[] FemaleName = { "Анна", "Дарья", "Мария", "Юлия", "Наталия", "Наталья", "Ксения", "Ирина", "Евгения", "Екатерина", "Руфь", "Полина", "Сара", "Мария", "Александра", "Евгения", "София" };
            string[] SurName = { "Пашкевич", "Мороз", "Занко", "Свибович", "Асенчик", "Муха", "Бут-Гусаим", "Шух", "Морозько", "Верхогляд", "Каханчик", "Федоренко", "Лещун", "Гапоненко", "Новичук", "Ялченко" };

            string[] MiddleName = { "Андреев", "Даниилов", "Данилов", "Алексеев", "Олегов", "Иванов", "Кириллов", "Владимиров", "Васильев", "Александров", "Максимов", "Дмитриев", "Валерьев", "Артемов" };

            //Страны
            string[] сountries = { "Армения", "Казахстан", "Беларусь", "Россия", "Турция", "США", "Франция", "Финляндия", "Бразилия", "Индия", "Конго", "ЮАР", "Серевная Корея", "Китай", "Канада", "Германия", "Египет", "Польша", "Украина", "Литва", "Латвия", "Нидерланды", "Дания", "Сирия", "Монголия", "Мексика", "Англия", "Португалия", "Италия", "Австрия", "Япония" };

            //Жанры фильмов
            string[] genres = { "Комедия", "Ужасы", "Боевик", "Приключение", "Триллер", "Мистика", "Детектив", "Биография", "Драма", "Мелодрама", "Фантастика", "Исторический", "Военный", "Семейный", "Вестерн", "Трагедия", "Фэнтэзи" };

            int actorsCount = 500;
            InitTableActors(db, actorsCount, MaleName, FemaleName, MiddleName, SurName, FemaleSuf, MaleSuf);

            int сountriesCount = InitTableCountryProductions(db, сountries);
            int genresCount = InitGenresTable(db, genres);

            int filmProductionsCount = 500;
            InitFilmProductions(db, filmProductionsCount, сountries);

            int filmsCount = 1000;
            InitTableFilms(db, filmProductionsCount, genresCount, сountriesCount, filmsCount);

            int actorsCasts = 500;
            InitTableActorCasts(db, actorsCount, filmsCount, actorsCasts);

            int staffsCount = 250;
            InitTableStaffs(db, staffsCount, MaleName, FemaleName, MiddleName, SurName, FemaleSuf, MaleSuf);

            int cinemaHallsCount = 3;
            int[] hallsPlaces = InitTableCinemaHalls(db, cinemaHallsCount);

            int eventsCount = 150;
            InitTableListEvents(db, eventsCount, filmsCount);

            int staffCastsCount = 500;
            InitTableStaffCasts(db, staffsCount, eventsCount, staffCastsCount);

            int placesCount = 250;
            InitTablePlaces(db, placesCount, hallsPlaces, eventsCount, cinemaHallsCount);
        }

        /// <summary>
        /// Генератор для таблицы Актеры
        /// </summary>
        public static void InitTableActors(CinemaContext db, int actorsCount, string[] MaleName, string[] FemaleName, string[] MiddleName, string[] SurName, string FemaleSuf, string MaleSuf)
        {
            if (!db.Actors.Any())
            {

                for (int i = 0; i < actorsCount; i += 2)
                {
                    string femaleMiddleName = MiddleName[randObj.Next(MiddleName.Length)] + FemaleSuf;
                    string maleMiddleName = MiddleName[randObj.Next(MiddleName.Length)] + MaleSuf;

                    string maleName = MaleName[randObj.Next(MaleName.Length)];
                    string femaleName = FemaleName[randObj.Next(FemaleName.Length)];

                    string surName = SurName[randObj.Next(SurName.Length)];

                    db.Add(new Actors { Name = maleName, Surname = surName, MiddleName = maleMiddleName });
                    db.Add(new Actors { Name = femaleName, Surname = surName, MiddleName = femaleMiddleName });
                }
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Генератор для таблицы Страна-производитель
        /// </summary>
        public static int InitTableCountryProductions(CinemaContext db, string[] сountries)
        {
            int countryCount = 0;
            if (!db.CountryProductions.Any())
            {
                countryCount = сountries.Length;
                for (int i = 0; i < countryCount; i++)
                {
                    db.Add(new CountryProductions { Name = сountries[i] });
                }
                db.SaveChanges();
            }
            return countryCount;
        }

        /// <summary>
        /// Генератор для таблицы Жанры
        /// </summary>
        public static int InitGenresTable(CinemaContext db, string[] genres)
        {
            int genresCount = 0;
            if (!db.Genres.Any())
            {
                genresCount = genres.Length;
                for (int i = 0; i < genresCount; i++)
                {
                    db.Add(new Genres { Name = genres[i] });
                }
                db.SaveChanges();
            }
            return genresCount;
        }

        /// <summary>
        /// Генератор для таблицы Компания-производитель
        /// </summary>
        public static void InitFilmProductions(CinemaContext db, int filmProductionCount, string[] сountries)
        {
            if (!db.FilmProductions.Any())
            {

                for (int i = 0; i < filmProductionCount; i++)
                {
                    string filmProduction = "Компания_" + i.ToString();
                    db.Add(new FilmProductions { Name = filmProduction, Country = сountries[randObj.Next(сountries.Length)] });
                }
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Генератор для таблицы Фильмы
        /// </summary>
        public static void InitTableFilms(CinemaContext db, int filmProductionCount, int genresCount, int сountriesCount, int filmsCount)
        {
            if (!db.Films.Any())
            {
                for (int i = 0; i < filmsCount; i++)
                {
                    string filmName = "Название_" + i.ToString();
                    int genreId = randObj.Next(1, genresCount);
                    int duration = randObj.Next(5, 230);
                    int filmProductionId = randObj.Next(1, filmProductionCount);
                    int countryProductionId = randObj.Next(1, сountriesCount);
                    int ageLimit = randObj.Next(0, 18);
                    string description = "Описание_" + i.ToString();
                    db.Add(new Films
                    {
                        Name = filmName,
                        GenreId = genreId,
                        Duration = duration,
                        FilmProductionId = filmProductionId,
                        CountryProductionId = countryProductionId,
                        AgeLimit = ageLimit,
                        Description = description
                    });
                }
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Генератор для таблицы Актерские группы
        /// </summary>
        public static void InitTableActorCasts(CinemaContext db, int actorCount, int filmCount, int actorCastCount)
        {
            if (!db.ActorCasts.Any())
            {
                for (int i = 0; i < actorCastCount; i++)
                {
                    int actorId = randObj.Next(1, actorCount);
                    int filmId = randObj.Next(1, filmCount);
                    db.Add(new ActorCasts { ActorId = actorId, FilmId = filmId });
                }
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Генератор для таблицы Сотрудники
        /// </summary>
        public static void InitTableStaffs(CinemaContext db, int staffsCount, string[] MaleName, string[] FemaleName, string[] MiddleName, string[] SurName, string FemaleSuf, string MaleSuf)
        {
            if (!db.Staffs.Any())
            {
                string[] posts = { "Старший кассир", "Младший кассир", "Стажер", "Бухгалтер", "Уборщик", "Директор", "Менеджер", "Рекламщик", "Переводчик" };
                for (int i = 0; i < staffsCount; i++)
                {
                    string femaleMiddleName = MiddleName[randObj.Next(MiddleName.Length)] + FemaleSuf;
                    string maleMiddleName = MiddleName[randObj.Next(MiddleName.Length)] + MaleSuf;

                    string maleName = MaleName[randObj.Next(MaleName.Length)];
                    string femaleName = FemaleName[randObj.Next(FemaleName.Length)];

                    string surName = SurName[randObj.Next(SurName.Length)];

                    int workExperience = randObj.Next(0, 450);
                    string post = posts[randObj.Next(posts.Length)];

                    db.Add(new Staffs { Name = maleName, Surname = surName, MiddleName = maleMiddleName, Post = post, WorkExperience = workExperience });
                    db.Add(new Staffs { Name = femaleName, Surname = surName, MiddleName = femaleMiddleName, Post = post, WorkExperience = workExperience });
                }
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Генератор для таблицы Залы
        /// </summary>
        public static int[] InitTableCinemaHalls(CinemaContext db, int cinemaHallsCount)
        {
            int[] hallsPlaces = new int[cinemaHallsCount];
            if (!db.CinemaHalls.Any())
            {
                for (int i = 0; i < cinemaHallsCount; i++)
                {
                    int hallNumber = i;
                    int maxPlaceNumber = randObj.Next(24, 64);
                    hallsPlaces[i] = maxPlaceNumber;
                    db.Add(new CinemaHalls { HallNumber = hallNumber, MaxPlaceNumber = maxPlaceNumber });
                }
                db.SaveChanges();
            }
            return hallsPlaces;
        }

        /// <summary>
        /// Генератор для таблицы События
        /// </summary>
        public static void InitTableListEvents(CinemaContext db, int eventsCount, int filmsCount)
        {
            if (!db.ListEvents.Any())
            {
                for (int i = 0; i < eventsCount; i++)
                {
                    string eventName = "Событие_" + i.ToString();
                    int filmId = randObj.Next(1, filmsCount);
                    decimal ticketPrice = randObj.Next(25, 125);

                    int randomMonth = randObj.Next(1, 12);
                    int randomDay = randObj.Next(1, DateTime.DaysInMonth(2022, randomMonth));
                    DateTime date = new DateTime(2022, randomMonth, randomDay);

                    int randomStartHour = randObj.Next(8, 20);
                    int randomStartMinute = randObj.Next(1, 59);
                    TimeSpan startTime = new TimeSpan(randomStartHour, randomStartMinute, 0);

                    int randomEndHour = randomStartHour + randObj.Next(0, 3);
                    int randomEndMinute = randomStartMinute + randObj.Next(1, 59);
                    TimeSpan endTime = new TimeSpan(randomEndHour, randomEndMinute, 0);

                    db.Add(new ListEvents {Name = eventName, Date = date, StartTime = startTime, EndTime = endTime, TicketPrice = ticketPrice, FilmId = filmId });
                }
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Генератор для таблицы Места
        /// </summary>
        public static void InitTablePlaces(CinemaContext db, int placesCount, int[] hallsPlaces, int eventsCount, int cinemaHallCount)
        {
            if (!db.Places.Any())
            {
                for (int i = 0; i < placesCount; i++)
                {
                    int listEventId = randObj.Next(1, eventsCount);
                    int cinemaHallId = randObj.Next(1, cinemaHallCount);

                    int placeNumber = randObj.Next(1, hallsPlaces[cinemaHallId]);

                    db.Add(new Places { ListEventId = listEventId, CinemaHallId = cinemaHallId, PlaceNumber = placeNumber, TakenSeat = true });
                }
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Генератор для таблицы группы Сотрудников
        /// </summary>
        public static void InitTableStaffCasts(CinemaContext db, int staffsCount, int eventsCount, int staffCastsCount)
        {
            if (!db.StaffCasts.Any())
                
                for (int i = 0; i < staffCastsCount; i++)
                {
                    int staffNumber = randObj.Next(1, staffsCount);
                    int eventNumber = randObj.Next(1, eventsCount);
                    db.Add(new StaffCasts { StaffId = staffNumber, ListEventId = eventNumber });
                }
                db.SaveChanges();
            }
        }     
}
