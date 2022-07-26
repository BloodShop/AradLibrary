using Library.DAL.SortBy;
using Library.Model;
using People.Model;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Library.DAL
{
    [Serializable]
    public class DataBase
    {
        public static List<IComparer<LibraryItem>> SortLibraryItem { get; private set; }

        [JsonPropertyName("LibraryItems")]
        public List<LibraryItem> LibraryItems { get; private set; } // Available Items

        [JsonPropertyName("People")]
        public List<Person> People { get; private set; }

        [JsonPropertyName("LoanedLibraryItems")]
        public Dictionary<LibraryItem, Person> LoanedLibraryItems { get; private set; } = new Dictionary<LibraryItem, Person>(); // Loaned Items ands owner

        // Singleton
        static DataBase _instance;
        public static DataBase Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DataBase();
                return _instance;
            }
        }
        private DataBase()
        {
            LibraryItems = new List<LibraryItem>();
            People = new List<Person>();
            Init();
        }
        static DataBase() =>
            SortLibraryItem = new List<IComparer<LibraryItem>>()
            {
                new CompareItemByPrice(),
                new CompareItemByPriceBackwards(),
                new CompareItemByTitle(),
                new CompareItemByTitleBackwards(),
                new CompareItemBySale(),
                new CompareItemByDate(),
                // Can add as many sotring critetia
            };

        void Init()
        {
            Init_Items();
            Init_People();
        }
        void Init_People()
        {
            var customer1 = new Customer("Alon", "123", new Address("Arad", "Irusim", 18, zip: 79082));
            var customer2 = new Customer("Bob Marley", "123", new Address("Beer-Sheva", "Salit", 2, zip: 99119));
            var customer3 = new Customer("Loli", "123", new Address("Beer-Sheva", "Ha-Kanayim", 40, zip: 00287));
            var customer4 = new Customer("Dani", "123", new Address("Ramot", "Salit", 2, zip: 67700));
            var customer5 = new Customer("Avraham", "123", new Address("Haifa", "Ha-Aliya", 55, zip: 80992));
            var customer6 = new Customer("Sagi", "123", new Address("Hulon", "Nofesh", 26, zip: 16734));
            var manager1 = new Manager("admin", "admin", new Address("Tel-Aviv", "Dizzingoff", 121, 'b', zip: 10192));
            var manager2 = new Manager("Manager", "admin", new Address("Rehovot", "Ha-Ofe", 13, 'C', zip: 66542));
            var employee1 = new Employee("Emp1", "admin", new Address("Eilat", "Leonardo", 92, zip: 11238));

            People.AddRange(new Person[] { customer1, customer2, customer3, customer4, customer5, customer6, manager1, manager2, employee1 });
        }
        void Init_Items()
        {
            #region ISBN Publishers
            ISBN.Publishers.Add("Anonymus", 666);
            ISBN.Publishers.Add("Penguin english library", 22);
            ISBN.Publishers.Add("Scribner", 2);
            ISBN.Publishers.Add("Big Cheese Books", 40);
            ISBN.Publishers.Add("Harper", 3);
            ISBN.Publishers.Add("S&S Books for Young Readers", 1);
            ISBN.Publishers.Add("Media Tie-In edition", 4);
            ISBN.Publishers.Add("Rockridge Press", 5);
            ISBN.Publishers.Add("Grand Central Publishing", 6);
            ISBN.Publishers.Add("Viking", 7);
            ISBN.Publishers.Add("Doubleday", 8);
            #endregion
            #region Countries Initializer
            ISBN.Countries.Add("Israel", 965);
            ISBN.Countries.Add("English", 1);
            ISBN.Countries.Add("French", 2);
            ISBN.Countries.Add("German", 3);
            ISBN.Countries.Add("Japanese", 4);
            ISBN.Countries.Add("Russian ", 5);
            ISBN.Countries.Add("Iran", 600);
            ISBN.Countries.Add("Kazakhstan", 601);
            ISBN.Countries.Add("Indonesia", 602);
            ISBN.Countries.Add("Saudi Arabia", 603);
            ISBN.Countries.Add("Vietnam", 604);
            ISBN.Countries.Add("Turkey", 605);
            ISBN.Countries.Add("Romania", 606);
            ISBN.Countries.Add("Mexico", 607);
            ISBN.Countries.Add("Macedonia", 608);
            ISBN.Countries.Add("Lithuania", 609);
            ISBN.Countries.Add("Thailand", 611);
            ISBN.Countries.Add("Peru", 612);
            ISBN.Countries.Add("Mauritius", 613);
            ISBN.Countries.Add("Lebanon", 614);
            ISBN.Countries.Add("Hungary", 615);
            ISBN.Countries.Add("Ukraine", 617);
            ISBN.Countries.Add("Greece", 618);
            ISBN.Countries.Add("Bulgaria", 619);
            ISBN.Countries.Add("Philippines", 621);
            ISBN.Countries.Add("People's Republic of China", 7);
            ISBN.Countries.Add("Former Czechoslovakia", 80);
            ISBN.Countries.Add("If", 81);
            ISBN.Countries.Add("Norway", 82);
            ISBN.Countries.Add("Poland", 83);
            ISBN.Countries.Add("Spain", 84);
            ISBN.Countries.Add("Brazil", 85);
            ISBN.Countries.Add("Former Yugoslavia", 86);
            ISBN.Countries.Add("Denmark", 87);
            ISBN.Countries.Add("Italy", 88);
            ISBN.Countries.Add("South Korea", 89);
            ISBN.Countries.Add("Netherlands", 90);
            ISBN.Countries.Add("Sweden", 91);
            ISBN.Countries.Add("International Publishers", 92);
            ISBN.Countries.Add("Argentina", 950);
            ISBN.Countries.Add("Finland", 951);
            ISBN.Countries.Add("Croatia", 953);
            ISBN.Countries.Add("Sri Lanka", 955);
            ISBN.Countries.Add("Chile", 956);
            ISBN.Countries.Add("Taiwan", 957);
            ISBN.Countries.Add("Colombia", 958);
            ISBN.Countries.Add("Cuba", 959);
            ISBN.Countries.Add("Slovenia", 961);
            ISBN.Countries.Add("Hongkong", 962);
            ISBN.Countries.Add("Malaysia", 967);
            ISBN.Countries.Add("Pakistan", 969);
            ISBN.Countries.Add("Portugal", 972);
            ISBN.Countries.Add("Caribbean Community", 976);
            ISBN.Countries.Add("Egypt", 977);
            ISBN.Countries.Add("Nigeria", 978);
            ISBN.Countries.Add("Venezuela", 980);
            ISBN.Countries.Add("Singapore", 981);
            ISBN.Countries.Add("South Pacific", 982);
            ISBN.Countries.Add("Bangladesh", 984);
            ISBN.Countries.Add("Belarus", 985);
            #endregion
            #region Book Known Authors
            Book.KnownAuthors.Add("Anonymus");
            Book.KnownAuthors.Add("Jane Austen");
            Book.KnownAuthors.Add("Harper Lee");
            Book.KnownAuthors.Add("F. Scott Fitzgerald");
            Book.KnownAuthors.Add("Jenny Han");
            Book.KnownAuthors.Add("Marlynn Jayme Schotland");
            Book.KnownAuthors.Add("Sarah Baker");
            Book.KnownAuthors.Add("Colleen Hoover");
            Book.KnownAuthors.Add("Amor Towles");
            Book.KnownAuthors.Add("Bonnie Garmus");
            #endregion
            #region JournalGenres Initializer
            Journal.JournalGenres.Add("Nature");
            Journal.JournalGenres.Add("Personal Letter");
            Journal.JournalGenres.Add("Greeting Card");
            Journal.JournalGenres.Add("Schedule / Things to Do List");
            Journal.JournalGenres.Add("Inner Monologue Representing Internal Conflicts");
            Journal.JournalGenres.Add("Classified or Personal Ads");
            Journal.JournalGenres.Add("Personal Essay or Philosophical Questions");
            Journal.JournalGenres.Add("Top Ten List/ Glossary or Dictionary");
            Journal.JournalGenres.Add("Poetry");
            Journal.JournalGenres.Add("Song Lyrics");
            Journal.JournalGenres.Add("Autobiographical Essay");
            Journal.JournalGenres.Add("Contest Entry Application");
            Journal.JournalGenres.Add("Business Letter or Correspondence/ Persuasive or Advocacy Letter");
            Journal.JournalGenres.Add("Biographical Summary");
            Journal.JournalGenres.Add("Critique of a Published Source");
            Journal.JournalGenres.Add("Speech or Debate");
            Journal.JournalGenres.Add("Historical Times Context Essay");
            Journal.JournalGenres.Add("Textbook Article");
            Journal.JournalGenres.Add("Science Article or Report / Business Article or Report");
            Journal.JournalGenres.Add("Lesson Plan");
            Journal.JournalGenres.Add("Encyclopedia Article");
            Journal.JournalGenres.Add("Short Scene from a Play with Notes for Stage Directions");
            Journal.JournalGenres.Add("Short Scene from a Movie with Notes for Camera Shots");
            Journal.JournalGenres.Add("Dialogue of a Conversation among Two or More People");
            Journal.JournalGenres.Add("Short Story");
            Journal.JournalGenres.Add("Adventure Magazine Story");
            Journal.JournalGenres.Add("Ghost Story");
            Journal.JournalGenres.Add("Myth, Tall Tale, or Fairy Tale");
            Journal.JournalGenres.Add("Talk Show Interview or Panel");
            Journal.JournalGenres.Add("Recipe and Description of Traditional Holiday Events");
            Journal.JournalGenres.Add("Classroom Discussion");
            Journal.JournalGenres.Add("Character Analysis or Case Study");
            Journal.JournalGenres.Add("Comedy Routine or Parody");
            Journal.JournalGenres.Add("Liner Notes");
            Journal.JournalGenres.Add("Picture book");
            Journal.JournalGenres.Add("Chart or Diagram with Explanation and Analysis");
            Journal.JournalGenres.Add("Brochure or Newsletter");
            Journal.JournalGenres.Add("Time Line or Chain of Events");
            Journal.JournalGenres.Add("Map with Explanation and Analysis");
            Journal.JournalGenres.Add("Magazine or TV Advertisement or Infomercial");
            Journal.JournalGenres.Add("Restaurant Description and Menu");
            Journal.JournalGenres.Add("Travel Brochure Description");
            Journal.JournalGenres.Add("How - To or Directions Booklet");
            Journal.JournalGenres.Add("Receipts, Applications, Deeds, Budgets or Other Documents");
            Journal.JournalGenres.Add("Wedding, Graduation or Special Event Invitation");
            Journal.JournalGenres.Add("Birth Certificate");
            Journal.JournalGenres.Add("Local News Report");
            Journal.JournalGenres.Add("Pop - Up book");
            Journal.JournalGenres.Add("Review and Poster for a Movie, Book, or TV Program");
            Journal.JournalGenres.Add("Board Game or Trivial Pursuit with Answers and Rules");
            Journal.JournalGenres.Add("Comic Strip or Graphic Novel excerpt");
            Journal.JournalGenres.Add("Power Point Presentation");
            Journal.JournalGenres.Add("Informational Video");
            Journal.JournalGenres.Add("Web Site");
            Journal.JournalGenres.Add("Future News Story");
            Journal.JournalGenres.Add("Letter to the Editor");
            Journal.JournalGenres.Add("Newspaper or Magazine Feature / Human Interest Story");
            Journal.JournalGenres.Add("Obituary, Eulogy or Tribute");
            Journal.JournalGenres.Add("News Program Story or Announcement");
            Journal.JournalGenres.Add("Tabloid Article");
            Journal.JournalGenres.Add("Other");
            #endregion
            #region BookGenres Initializer
            Book.BookGenres.Add("Action and adventure");
            Book.BookGenres.Add("Art / architecture");
            Book.BookGenres.Add("Alternate history");
            Book.BookGenres.Add("Autobiography");
            Book.BookGenres.Add("Anthology");
            Book.BookGenres.Add("Biography");
            Book.BookGenres.Add("Chick lit");
            Book.BookGenres.Add("Business / economics");
            Book.BookGenres.Add("Children's");
            Book.BookGenres.Add("Crafts / hobbies");
            Book.BookGenres.Add("Classic");
            Book.BookGenres.Add("Cookbook");
            Book.BookGenres.Add("Comic book");
            Book.BookGenres.Add("Diary");
            Book.BookGenres.Add("Coming-of-age");
            Book.BookGenres.Add("Dictionary");
            Book.BookGenres.Add("Crime");
            Book.BookGenres.Add("Encyclopedia");
            Book.BookGenres.Add("Drama");
            Book.BookGenres.Add("Guide");
            Book.BookGenres.Add("Fairytale");
            Book.BookGenres.Add("Health / fitness");
            Book.BookGenres.Add("Fantasy");
            Book.BookGenres.Add("History");
            Book.BookGenres.Add("Graphic novel");
            Book.BookGenres.Add("Home and garden");
            Book.BookGenres.Add("Historical fiction");
            Book.BookGenres.Add("Humor");
            Book.BookGenres.Add("Horror");
            Book.BookGenres.Add("Journal");
            Book.BookGenres.Add("Mystery");
            Book.BookGenres.Add("Math");
            Book.BookGenres.Add("Paranormal romance");
            Book.BookGenres.Add("Memoir");
            Book.BookGenres.Add("Picture book");
            Book.BookGenres.Add("Philosophy");
            Book.BookGenres.Add("Poetry");
            Book.BookGenres.Add("Prayer");
            Book.BookGenres.Add("Political thriller");
            Book.BookGenres.Add("Religion, spirituality, and new age");
            Book.BookGenres.Add("Romance");
            Book.BookGenres.Add("Textbook");
            Book.BookGenres.Add("Satire");
            Book.BookGenres.Add("True crime");
            Book.BookGenres.Add("Science fiction");
            Book.BookGenres.Add("Review");
            Book.BookGenres.Add("Short story");
            Book.BookGenres.Add("Science");
            Book.BookGenres.Add("Suspense");
            Book.BookGenres.Add("Self help");
            Book.BookGenres.Add("Thriller");
            Book.BookGenres.Add("Sports and leisure");
            Book.BookGenres.Add("Western");
            Book.BookGenres.Add("Travel");
            Book.BookGenres.Add("Young adult");
            Book.BookGenres.Add("True crime");
            #endregion

            #region Initialize Books
            #region Pride and Prejudice
            var book1 = new Book("Pride and Prejudice", new DateTime(2014, 7, 8), 20.5, 331);
            book1.Authors.Add("Jane Austen");
            book1.Publisher = "Harper";
            book1.Synopsis = "One of the most cherished stories of all time, To Kill a Mockingbird has been translated into more than forty languages, sold more than forty million copies worldwide, served as the basis for an enormously popular motion picture, and was voted one of the best novels of the twentieth century by librarians across the country. A gripping, heart-wrenching, and wholly remarkable tale of coming-of-age in a South poisoned by virulent prejudice, it views a world of great beauty and savage inequities through the eyes of a young girl, as her father—a crusading local lawyer—risks everything to defend a black man unjustly accused of a terrible crime.";
            book1.Genres.Add("Action and adventure");
            book1.Genres.Add("Classic");
            book1.Genres.Add("Drama");
            book1.Genres.Add("Historical fiction");
            //book1.ImageCover.Source = new BitmapImage(new Uri("ms-appx:///Assets/Images/Covers/Pride and Prejudice.png"));
            book1.ImageSource = new Uri("ms-appx:///Assets/Images/Covers/Pride and Prejudice.png");
            book1.Revision = 103;
            book1.AddReview(new Review("Lilac Roe Pin", 1, new DateTime(2018, 7, 24), "Wonderful classic and great clean copy! Picture included for condition & reference of version I purchased. I wore it out a little from taking it everywhere as I read. I recommend this version!"));
            book1.AddReview(new Review("abhishek k .kamath", 1, new DateTime(2018, 7, 24), "Whenever good reads shows this books on the top list , I wondered whats in this book that people rate this much for it.\nBut finally I had a chance of reading this and reading after this I felt like I would give more stars than possible .\nThe patience is utter key in the book. The way every character progress , the way harper Lee have developed each character it's real more than fiction."));
            Manager.SetSaleEventHandler += book1.OnSetSale;
            Manager.EndSaleEventHandler += book1.OnEndSale;
            #endregion
            #region To Kill A Mockingbird
            var book2 = new Book("To Kill A Mockingbird", new DateTime(2015, 4, 6), 90.2, 145);
            book2.Authors.Add("Harper Lee");
            book2.Publisher = "Scribner";
            book2.Synopsis = "When Elizabeth Bennet first meets eligible bachelor Fitzwilliam Darcy, she thinks him arrogant and conceited; he is indifferent to her good looks and lively mind. When she later discovers that Darcy has involved himself in the troubled relationship between his friend Bingley and her beloved sister Jane, she is determined to dislike him more than ever. In the sparkling comedy of manners that follows, Jane Austen shows the folly of judging by first impressions and superbly evokes the friendships, gossip and snobberies of provincial middle-class life.";
            book2.Genres.Add("Young adult");
            book2.Genres.Add("Classic");
            book2.Genres.Add("Drama");
            book2.Genres.Add("Historical fiction");
            //book2.ImageCover.Source = new BitmapImage(new Uri("ms-appx:///Assets/Images/Covers/To Kill a Mockingbird.png"));
            book2.ImageSource = new Uri("ms-appx:///Assets/Images/Covers/To Kill a Mockingbird.png");
            book2.Revision = 156;
            book2.AddReview(new Review("Lima Bean", 1, new DateTime(2018, 7, 24), "There must be many thousands of digital versions of Jane Austen's Pride and Prejudice. I got this because I wanted to compare it with another that I had, and didn't realize until it was on my kindle that the name of the author on the cover page is incorrect!! The author is NOT Charlotte Bronte!!!!! This is so inexplicable and appalling a mistake, I had to write and mention it so that it can (hopefully) be corrected. The rest of the text is fine. I don't want it on my kindle, and have deleted it. How anyone could have confused Jane Austen and Charlotte Bronte (author of Jane Eyre) is beyond me."));
            book2.AddReview(new Review("Review Man", 1, new DateTime(2020, 8, 4), "I bought \"Pride and Prejudice\" from them and when the book arrived it was the size of a magazine. I've never seen such a small font in a book. It was like trying to read the back of a medicine bottle. Then I bought \"Animal Farm\" from them and didn't start reading it until the return window had passed. I get to page 116 and find I'm missing 32 pages ... next page after 116 was 133. So now what? It was only $9 but sheesh. I won't be buying any more books from theses people."));
            book2.AddReview(new Review("KL", 5, new DateTime(2018, 10, 29), "Well it's a classic and my most favourite !! So had to get this in the leather bound edition.\nIf you looking for a leather bound edition this is perfect and if it's not in stock, contact the seller and they'll surely help you out and they are very good!!"));
            Manager.SetSaleEventHandler += book2.OnSetSale;
            Manager.EndSaleEventHandler += book2.OnEndSale;
            #endregion
            #region The Great Gatsby
            var book3 = new Book("The Great Gatsby", new DateTime(2003, 5, 27), 30.5, 145);
            book3.Authors.Add("F. Scott Fitzgerald");
            book3.Authors.Add("DR. HomeMade Gross");
            book3.Publisher = "Penguin english library";
            book3.Synopsis = "Young, handsome and fabulously rich, Jay Gatsby is the bright star of the Jazz Age, but as writer Nick Carraway is drawn into the decadent orbit of his Long Island mansion, where the party never seems to end, he finds himself faced by the mystery of Gatsby's origins and desires. Beneath the shimmering surface of his life, Gatsby is hiding a secret: a silent longing that can never be fulfilled. And soon, this destructive obsession will force his world to unravel.";
            book3.Genres.Add("Classic");
            book3.Genres.Add("Drama");
            book3.Genres.Add("Historical fiction");
            book3.ImageSource = new Uri("ms-appx:///Assets/Images/Covers/The Great Gatsby.png");
            book3.Revision = 53;
            book3.AddReview(new Review("Alon", 5, DateTime.Now, "I became familiar with the works of F. Scott Fitzgerald back in high school when for my literature assignment I had to read \"Winter Dreams\", I enjoyed it but have never read the book that made him famous--good thing this exists."));
            book3.AddReview(new Review("M Ishak", 2, new DateTime(2015, 5, 5), "All flash bang and no substance. The fusion of new music and roaring 20s elements is a travesty to the spirit of Fitzgeralds book. Tobey Maguire as Nick Carraway is too smug and too conscious of his role unlike Sam Waterston who played it better in the 1974 version."));
            book3.AddReview(new Review("Karbear", 1, new DateTime(2019, 2, 5), " grew up hearing about The Great Gatsby but somehow never had to read it in any English course. I finally read it after college and am confused as to why it's a classic. It's an easy read, that's about the only positive thing I can say about it. I once saw a tweet saying something along the lines of, \"I have to assume that anyone who has a Great Gatsby themed party never finished the book.\" It still makes me chuckle. It's an awful and depressing story with no redeeming qualities. And don't forget the drunk driving! Pfft."));
            book3.AddReview(new Review("Eric H. Malloy", 1, new DateTime(2019, 9, 2), "So I thought I should read some of the classics once in a while, to improve my mind, expand my knowledge, etc.\nSo having now read 'The Great Gatsby', I don't know how it got to be a classic.\nBad writing, long run on pompous sentences, totally boring characters and very little plot don't add up to a classic in my opinion.\nMostly it's plot, what little there is, involves some rich full of themselves people trying to score some booze and looking for a party during prohibition.\nWhat is somewhat interesting is the description of daily life in the U.S. in the 1920s.The technology, clothes, hairstyles, and attitudes.Otherwise I would say why bother, unless you want to cross a \"classic\" off your reading bucket list."));
            book3.AddReview(new Review("Nenia Campbell", 5, new DateTime(2021, 5, 21), "It's a shame that Fitzgerald never lived to see his novel become one of the most successful literary works of all time. In fact, according to the afterword in this text, by the time of his death, his book had all but fallen out of circulation. I was inspired to reread THE GREAT GATSBY after reading Ernest Hemingway's A MOVEABLE FEAST, in which a detailed account of Fitzgerald talking to Hemingway about his \"new\" novel, The Great Gatsby, transpires. Hemingway is impressed by the quality of the book and declares it exceptionally good but also notes that Fitzgerald laments his bewilderingly tepid sales.THE GREAT GATSBY is narrated in first person by a man named Nick Carraway, who seems to be a stand-in for Fitzgerald himself: educated, but not possessing much money, and hobnobbing it with those who are much more privileged than he is. His cousin is Daisy Buchanan, who is married to a bigoted narcissist named Tom, who is also having an affair with a married woman. Charming.Nick's neighbor is a nouveau riche man named Gatsby who is well known in the area for throwing incredibly lavish parties that people attend with the same sort of wide-eyed wonder as one would a theme park. Unbeknownst to Nick, the overtures of friendship Gatsby extends his way aren't exactly guileless; Gatsby is utterly obsessed with Daisy and has been for years, and would like for Nick to arrange for the two of them to meet, as it turns out that they had a relationship when they were young and Gatsby was poor, and he's thought of her ever since. They meet and Daisy is as stunned by his lavish displays of wealth as everyone else, and also remembers all the good times she had with young Gatsby, and the two of them begin an affair of their own.This is a tragedy that is also about classism, and how good breeding often excuses the rich. It's also a tale of love and obsession, and how passion can quite literally consume those who open themselves up to consumption. Gatsby's money attracts people to him, as does his charm, but he never really lets anyone know him except for Daisy, who is so selfish in her love that she isn't really ever quite willing to give of herself to anyone. Even her own child feels like an afterthought, mentioned only once. One really can't help but feel like Daisy is in it mostly for Daisy and doesn't give a fig for anyone else.The most sympathetic person in the book is actually Nick, whose love and admiration for Gatsby is of a much purer form than that, ironically, which Daisy has for him. Nick is a stand-in for the reader, who discovers the story in pieces in real time, as we do, when disaster inevitably causes all of these fractures to cave in. Gatsby never really understands that what he is in love with is an illusion and a projection of his own wishes and desires. I felt kind of like Daisy is an extension of his desire to be one of the rich, and that his desire to marry her stems partially from his desire to be fully accepted into high society.The writing in THE GREAT GATSBY is truly gorgeous and had me immediately buying up some of Fitzgerald's other works. I read this when I was a teen and much of the nuance was lost on me (and I also struggled with the vocabulary). I remember giving this a three-star rating originally, fixating mostly on the romance and missing basically everything else (as teens can sometimes do). THE GREAT GATSBY definitely gets better over time. I can see and understand the allegations of antisemitism within this work (there are other archaic references to people of other ethnicities that would be considered highly offensive now), and while the age of this book doesn't excuse those words and descriptions used, I do think that the context and the time in which this book was written makes it easier to understand why they are there. What a stunning portrait of doomed love. I am in awe."));
            Manager.SetSaleEventHandler += book3.OnSetSale;
            Manager.EndSaleEventHandler += book3.OnEndSale;
            #endregion
            #region The Summer I Turned Pretty
            var book4 = new Book("The Summer I Turned Pretty", new DateTime(2022, 5, 5), 9.23, 202);
            book4.Authors.Add("Jenny Han");
            book4.Publisher = "S&S Books for Young Readers";
            book4.Synopsis = "Belly measures her life in summers. Everything good, everything magical happens between the months of June and August. Winters are simply a time to count the weeks until the next summer, a place away from the beach house, away from Susannah, and most importantly, away from Jeremiah and Conrad. They are the boys that Belly has known since her very first summer—they have been her brother figures, her crushes, and everything in between. But one summer, one wonderful and terrible summer, the more everything changes, the more it all ends up just the way it should have been all along.";
            book4.Genres.Add("Fantasy");
            book4.ImageSource = new Uri("ms-appx:///Assets/Images/Covers/The Summer I Turned Pretty.png");
            book4.Revision = 2;
            book4.AddReview(new Review("Alon", 5, DateTime.Now, "I became familiar with the works of F. Scott Fitzgerald back in high school when for my literature assignment I had to read \"Winter Dreams\", I enjoyed it but have never read the book that made him famous--good thing this exists."));
            book4.AddReview(new Review("M Ishak", 2, new DateTime(2015, 5, 5), "All flash bang and no substance. The fusion of new music and roaring 20s elements is a travesty to the spirit of Fitzgeralds book. Tobey Maguire as Nick Carraway is too smug and too conscious of his role unlike Sam Waterston who played it better in the 1974 version."));
            book4.AddReview(new Review("Karbear", 1, new DateTime(2019, 2, 5), " grew up hearing about The Great Gatsby but somehow never had to read it in any English course. I finally read it after college and am confused as to why it's a classic. It's an easy read, that's about the only positive thing I can say about it. I once saw a tweet saying something along the lines of, \"I have to assume that anyone who has a Great Gatsby themed party never finished the book.\" It still makes me chuckle. It's an awful and depressing story with no redeeming qualities. And don't forget the drunk driving! Pfft."));
            book4.AddReview(new Review("Eric H. Malloy", 1, new DateTime(2019, 9, 2), "So I thought I should read some of the classics once in a while, to improve my mind, expand my knowledge, etc.\nSo having now read 'The Great Gatsby', I don't know how it got to be a classic.\nBad writing, long run on pompous sentences, totally boring characters and very little plot don't add up to a classic in my opinion.\nMostly it's plot, what little there is, involves some rich full of themselves people trying to score some booze and looking for a party during prohibition.\nWhat is somewhat interesting is the description of daily life in the U.S. in the 1920s.The technology, clothes, hairstyles, and attitudes.Otherwise I would say why bother, unless you want to cross a \"classic\" off your reading bucket list."));
            book4.AddReview(new Review("Nenia Campbell", 5, new DateTime(2021, 5, 21), "It's a shame that Fitzgerald never lived to see his novel become one of the most successful literary works of all time. In fact, according to the afterword in this text, by the time of his death, his book had all but fallen out of circulation. I was inspired to reread THE GREAT GATSBY after reading Ernest Hemingway's A MOVEABLE FEAST, in which a detailed account of Fitzgerald talking to Hemingway about his \"new\" novel, The Great Gatsby, transpires. Hemingway is impressed by the quality of the book and declares it exceptionally good but also notes that Fitzgerald laments his bewilderingly tepid sales.THE GREAT GATSBY is narrated in first person by a man named Nick Carraway, who seems to be a stand-in for Fitzgerald himself: educated, but not possessing much money, and hobnobbing it with those who are much more privileged than he is. His cousin is Daisy Buchanan, who is married to a bigoted narcissist named Tom, who is also having an affair with a married woman. Charming.Nick's neighbor is a nouveau riche man named Gatsby who is well known in the area for throwing incredibly lavish parties that people attend with the same sort of wide-eyed wonder as one would a theme park. Unbeknownst to Nick, the overtures of friendship Gatsby extends his way aren't exactly guileless; Gatsby is utterly obsessed with Daisy and has been for years, and would like for Nick to arrange for the two of them to meet, as it turns out that they had a relationship when they were young and Gatsby was poor, and he's thought of her ever since. They meet and Daisy is as stunned by his lavish displays of wealth as everyone else, and also remembers all the good times she had with young Gatsby, and the two of them begin an affair of their own.This is a tragedy that is also about classism, and how good breeding often excuses the rich. It's also a tale of love and obsession, and how passion can quite literally consume those who open themselves up to consumption. Gatsby's money attracts people to him, as does his charm, but he never really lets anyone know him except for Daisy, who is so selfish in her love that she isn't really ever quite willing to give of herself to anyone. Even her own child feels like an afterthought, mentioned only once. One really can't help but feel like Daisy is in it mostly for Daisy and doesn't give a fig for anyone else.The most sympathetic person in the book is actually Nick, whose love and admiration for Gatsby is of a much purer form than that, ironically, which Daisy has for him. Nick is a stand-in for the reader, who discovers the story in pieces in real time, as we do, when disaster inevitably causes all of these fractures to cave in. Gatsby never really understands that what he is in love with is an illusion and a projection of his own wishes and desires. I felt kind of like Daisy is an extension of his desire to be one of the rich, and that his desire to marry her stems partially from his desire to be fully accepted into high society.The writing in THE GREAT GATSBY is truly gorgeous and had me immediately buying up some of Fitzgerald's other works. I read this when I was a teen and much of the nuance was lost on me (and I also struggled with the vocabulary). I remember giving this a three-star rating originally, fixating mostly on the romance and missing basically everything else (as teens can sometimes do). THE GREAT GATSBY definitely gets better over time. I can see and understand the allegations of antisemitism within this work (there are other archaic references to people of other ethnicities that would be considered highly offensive now), and while the age of this book doesn't excuse those words and descriptions used, I do think that the context and the time in which this book was written makes it easier to understand why they are there. What a stunning portrait of doomed love. I am in awe."));
            Manager.SetSaleEventHandler += book4.OnSetSale;
            Manager.EndSaleEventHandler += book4.OnEndSale;
            #endregion
            #region The Super Easy Teen Baking Cookbook
            var book5 = new Book("The Super Easy Teen Baking Cookbook", new DateTime(2021, 8, 31), 12.78, 148);
            book5.Authors.Add("Marlynn Jayme Schotland");
            book5.Publisher = "Rockridge Press";
            book5.Synopsis = "Baking can seem like a lot of complicated chemistry, but with the help of The Super Easy Teen Baking Cookbook, it all becomes simple. These beginner-friendly recipes show teens how to create their own sweet and savory baked goods at home―even if they’ve never baked before. There’s no time-consuming prep work, no boring flavors, and no help from adults necessary!";
            book5.Genres.Add("Cookbook");
            book5.ImageSource = new Uri("ms-appx:///Assets/Images/Covers/The Super Easy Teen Baking Cookbook.png");
            book5.Revision = 1;
            book5.AddReview(new Review("David", 5, DateTime.Now, "I became familiar with the works of F. Scott Fitzgerald back in high school when for my literature assignment I had to read \"Winter Dreams\", I enjoyed it but have never read the book that made him famous--good thing this exists."));
            book5.AddReview(new Review("Robi", 2, new DateTime(2022, 5, 5), "All flash bang and no substance. The fusion of new music and roaring 20s elements is a travesty to the spirit of Fitzgeralds book. Tobey Maguire as Nick Carraway is too smug and too conscious of his role unlike Sam Waterston who played it better in the 1974 version."));
            Manager.SetSaleEventHandler += book5.OnSetSale;
            Manager.EndSaleEventHandler += book5.OnEndSale;
            #endregion
            #region Vegetarian Cookbook for Teens
            var book6 = new Book("Vegetarian Cookbook for Teens", new DateTime(2021, 8, 31), 12.78, 148);
            book6.Authors.Add("Sarah Baker");
            book6.Publisher = "Rockridge Press";
            book6.Synopsis = "Whatever your reason for exploring vegetarian cooking―your love of animals, reducing your carbon footprint, or the variety of health benefits―the Vegetarian Cookbook for Teens will show you that meatless cooking goes way beyond a life of sprouts and salads. In fact, vegetarian cooking can be fun, easy, flavorful, and lead to a lifetime of wellness.\nThis cookbook for teens is filled with delicious recipes like Sweet Walnut Apple Salad and Avocado Chickpea Pasta Bowl that will help you gain confidence in the kitchen, regardless of experience, with step - by - step guidance.From how to create a savory breakfast burrito in five minutes flat, to how to simmer up a hearty vegetarian chili with your own creative twist, these delicious creations may even encourage your parents, siblings, and friends to move to more of a vegetarian lifestyle.";
            book6.Genres.Add("Cookbook");
            book6.ImageSource = new Uri("ms-appx:///Assets/Images/Covers/Vegetarian Cookbook for Teens.png");
            book6.Revision = 44;
            book6.AddReview(new Review("Rich Dreiman", 5, DateTime.Now, "I hardly has any pictures. Especially for a preteen to see what the finished product should look like. It's just another cookbook"));
            Manager.SetSaleEventHandler += book6.OnSetSale;
            Manager.EndSaleEventHandler += book6.OnEndSale;
            #endregion
            #region Verity
            var book7 = new Book("Verity", new DateTime(2021, 10, 26), 11.26, 336);
            book7.Authors.Add("Colleen Hoover");
            book7.Publisher = "Grand Central Publishing";
            book7.Synopsis = "Whose truth is the lie? Stay up all night reading the sensational psychological thriller that has readers obsessed, from the #1 New York Times bestselling author of It Ends With Us.\nLowen Ashleigh is a struggling writer on the brink of financial ruin when she accepts the job offer of a lifetime. Jeremy Crawford, husband of bestselling author Verity Crawford, has hired Lowen to complete the remaining books in a successful series his injured wife is unable to finish.\nLowen arrives at the Crawford home, ready to sort through years of Verity’s notes and outlines, hoping to find enough material to get her started. What Lowen doesn’t expect to uncover in the chaotic office is an unfinished autobiography Verity never intended for anyone to read.Page after page of bone - chilling admissions, including Verity's recollection of the night her family was forever altered.\nLowen decides to keep the manuscript hidden from Jeremy, knowing its contents could devastate the already grieving father.But as Lowen’s feelings for Jeremy begin to intensify, she recognizes all the ways she could benefit if he were to read his wife’s words. After all, no matter how devoted Jeremy is to his injured wife, a truth this horrifying would make it impossible for him to continue loving her.";
            book7.Genres.Add("True crime");
            book7.ImageSource = new Uri("ms-appx:///Assets/Images/Covers/Verity.png");
            book7.Revision = 54;
            book7.AddReview(new Review("Tarryn Fisher", 5, DateTime.Now, "Sublimely creepy with a true Hoover pulse. I’ve been waiting for a thriller like this for years."));
            book7.AddReview(new Review("Claire Contreras", 5, DateTime.Now, "Riveting and unexpected.Impossible to put down."));
            Manager.SetSaleEventHandler += book7.OnSetSale;
            Manager.EndSaleEventHandler += book7.OnEndSale;
            #endregion
            #region The Lincoln Highway: A Novel
            var book8 = new Book("The Lincoln Highway: A Novel", new DateTime(2021, 10, 26), 11.26, 336);
            book8.Authors.Add("Amor Towles");
            book8.Publisher = "Viking";
            book8.Synopsis = "In June, 1954, eighteen-year-old Emmett Watson is driven home to Nebraska by the warden of the juvenile work farm where he has just served fifteen months for involuntary manslaughter. His mother long gone, his father recently deceased, and the family farm foreclosed upon by the bank, Emmett's intention is to pick up his eight-year-old brother, Billy, and head to California where they can start their lives anew. But when the warden drives away, Emmett discovers that two friends from the work farm have hidden themselves in the trunk of the warden's car. Together, they have hatched an altogether different plan for Emmett's future, one that will take them all on a fateful journey in the opposite direction—to the City of New York.\nSpanning just ten days and told from multiple points of view, Towles's third novel will satisfy fans of his multi-layered literary styling while providing them an array of new and richly imagined settings, characters, and themes.";
            book8.Genres.Add("Western");
            book8.ImageSource = new Uri("ms-appx:///Assets/Images/Covers/TheLincolnHighwayANovel.png");
            book8.Revision = 54;
            book8.AddReview(new Review("Compulsive Reader", 2, DateTime.Now, "Try as I might, I failed to generate a spark of interest in any of the characters or their aimless wanderings. A Gentleman in Moscow, though set almost entirely in one building, had a great deal more action and interest."));
            book8.AddReview(new Review("M. G. Ball", 1, DateTime.Now, "The principles are teenage boys in the early 1950s--except for an eight-year-old little brother. The narrator ricochets from one of the characters to another, their thoughts (except, maybe, for Emmett’s) far too mature and introspective considering the ages involved. Moreover, the little brother is beyond precocious. The plot meanders, soon becomes tedious, then devolves into the absurd. I gave up…"));
            Manager.SetSaleEventHandler += book8.OnSetSale;
            Manager.EndSaleEventHandler += book8.OnEndSale;
            #endregion
            #region Lessons in Chemistry: A Novel
            var book9 = new Book("Lessons in Chemistry: A Novel", new DateTime(2022, 4, 5), 11.26, 336);
            book9.Authors.Add("Bonnie Garmus");
            book9.Publisher = "Doubleday";
            book9.Synopsis = "Chemist Elizabeth Zott is not your average woman. In fact, Elizabeth Zott would be the first to point out that there is no such thing as an average woman. But it’s the early 1960s and her all-male team at Hastings Research Institute takes a very unscientific view of equality. Except for one: Calvin Evans; the lonely, brilliant, Nobel–prize nominated grudge-holder who falls in love with—of all things—her mind. True chemistry results. \nBut like science, life is unpredictable.Which is why a few years later Elizabeth Zott finds herself not only a single mother, but the reluctant star of America’s most beloved cooking show Supper at Six. Elizabeth’s unusual approach to cooking(“combine one tablespoon acetic acid with a pinch of sodium chloride”) proves revolutionary. But as her following grows, not everyone is happy.Because as it turns out, Elizabeth Zott isn’t just teaching women to cook. She’s daring them to change the status quo.  \nLaugh -out-loud funny, shrewdly observant, and studded with a dazzling cast of supporting characters, Lessons in Chemistry is as original and vibrant as its protagonist.";
            book9.Genres.Add("Science fiction");
            book9.ImageSource = new Uri("ms-appx:///Assets/Images/Covers/LessonsinChemistryANovel.png");
            book9.Revision = 89;
            book9.AddReview(new Review("BuzzFeed", 2, DateTime.Now, "A kicky debut, this book tackles feminism, resilience, and rationalism in a fun and refreshing way."));
            book9.AddReview(new Review("Claire Lombardo", 1, DateTime.Now, "Lessons in Chemistry is a breath of fresh air—a witty, propulsive, and refreshingly hopeful novel populated with singular characters. This book is an utter delight—wry, warm, and compulsively readable."));
            Manager.SetSaleEventHandler += book9.OnSetSale;
            Manager.EndSaleEventHandler += book9.OnEndSale;
            #endregion
            #endregion
            #region Initialize Journals
            var journal1 = new Journal("Nature", new DateTime(2000, 2, 24), 30.5, 31);
            journal1.Editors.AddRange(new[] { "Bob Dylen" });
            journal1.Genres.AddRange(new[] { "Nature", "Chart or Diagram with Explanation and Analysis" });
            journal1.Contributors.AddRange(new[] { "Smol JP", "Xie P" });
            journal1.Frequency = JournalFrequency.Weekly;
            journal1.ImageSource = new Uri("ms-appx:///Assets/Images/Covers/NatureJournalAKid'sNatureJournal.png");
            Manager.SetSaleEventHandler += journal1.OnSetSale;
            Manager.EndSaleEventHandler += journal1.OnEndSale;

            var journal2 = new Journal("The Wall Street Journal", new DateTime(2020, 9, 8), 28.99, 22);
            journal2.Editors.AddRange(new[] { "Dow Jones", "Company Inc." });
            journal2.Genres.AddRange(new[] { "Receipts, Applications, Deeds, Budgets or Other Documents" });
            journal2.Contributors.AddRange(new[] { "Effler SW", "Rodriguez-Iturbe I" });
            journal2.Frequency = JournalFrequency.Daily;
            journal2.ImageSource = new Uri("ms-appx:///Assets/Images/Covers/TheWallStreetJournal.png");
            journal2.AddReview(new Review("Lulu", 1, new DateTime(2015, 10, 7), "Electronic edition was missing many articles contained in the print edition. I would never purchase the electronic addition again; it is a rip-off."));
            journal2.AddReview(new Review("Amazon User", 1, new DateTime(2018, 11, 11), "I was excited to try the WSJ on my Kindle, but was disappointed with the offerings. Articles pushed to the Kindle do not mildly equate to what is offered via the app or website. For close to $30 a month, this sticker price is pretty steep for what you actually get. I get that the WSJ is a prestigious newspaper, but this is price gouging IMO."));
            Manager.SetSaleEventHandler += journal2.OnSetSale;
            Manager.EndSaleEventHandler += journal2.OnEndSale;

            var journal3 = new Journal("The Journal of Snow Woman", new DateTime(2017, 12, 17), 14.95, 40);
            journal3.Editors.AddRange(new[] { "CR Wilson" });
            journal3.Genres.AddRange(new[] { "Power Point Presentation" });
            journal3.Contributors.AddRange(new[] { "Hecky RE", "Ebener MP" });
            journal3.Frequency = JournalFrequency.SemiAnnualy;
            journal3.ImageSource = new Uri("ms-appx:///Assets/Images/TheJournalofSnowWoman.png");
            journal3.AddReview(new Review("Rosemary Ymzon", 5, new DateTime(2022, 2, 15), "Without a lot of descriptive words the author succeeds in leading the reader thru his story of what life could have been like on the western prairie in the late 1800’s. This story is told well and with respect for the People who lived in North America a thousand years before Europeans arrived."));
            journal3.AddReview(new Review("Kindle Customer", 5, new DateTime(2022, 1, 4), "This well written story shows what really makes a real family, acting with love and loyalty. My sympathies have always been with the Native American people who had their lands and proud way of life stolen from them because of greed, hatred, and unfounded fear."));
            Manager.SetSaleEventHandler += journal3.OnSetSale;
            Manager.EndSaleEventHandler += journal3.OnEndSale;

            var journal4 = new Journal("Classic Cottages", new DateTime(2020, 1, 10), 14.99, 31);
            journal4.Editors.AddRange(new[] { "Hoffman Media" });
            journal4.Genres.AddRange(new[] { "Tabloid Article", "News Program Story or Announcement" });
            journal4.Contributors.AddRange(new[] { "MCDonnell JJ", "KOong FX", "Cirpka OA" });
            journal4.Frequency = JournalFrequency.BiMonthly;
            journal4.ImageSource = new Uri("ms-appx:///Assets/Images/Covers/ClassicCottages.png");
            Manager.SetSaleEventHandler += journal4.OnSetSale;
            Manager.EndSaleEventHandler += journal4.OnEndSale;


            var journal6 = new Journal("White Wolf Composition Notebook, Wide Ruled", new DateTime(2020, 1, 10), 14.99, 31);
            journal6.Editors.AddRange(new[] { "Rabota Man" });
            journal6.Genres.AddRange(new[] { "Tabloid Article" });
            journal6.Contributors.AddRange(new[] { "Bokit JJ", "rA FX", "DafT OA" });
            journal6.Frequency = JournalFrequency.Qurterly;
            journal6.ImageSource = new Uri("ms-appx:///Assets/Images/Covers/Wolves.png");
            Manager.SetSaleEventHandler += journal6.OnSetSale;
            Manager.EndSaleEventHandler += journal6.OnEndSale;

            var journal5 = new Journal("Amazing Swim Coach", new DateTime(2001, 5, 11), 15.56, 99);
            journal5.Editors.AddRange(new[] { "Rofish Lobster" });
            journal5.Genres.AddRange(new[] { "News Program Story or Announcement" });
            journal5.Contributors.AddRange(new[] { "MCDonnell JJ", "KOong FX", "Cirpka OA" });
            journal5.Frequency = JournalFrequency.Daily;
            journal5.ImageSource = new Uri("ms-appx:///Assets/Images/AmazingSwimCoach.png");
            Manager.SetSaleEventHandler += journal5.OnSetSale;
            Manager.EndSaleEventHandler += journal5.OnEndSale;
            #endregion

            LibraryItems.AddRange(new LibraryItem[] { book1, book2, book3, book4, book5, book6, book7, book8, book9, journal1, journal2, journal3, journal4, journal5, journal6 });
        }
    }
}