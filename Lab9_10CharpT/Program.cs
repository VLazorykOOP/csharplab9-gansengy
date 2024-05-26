using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Lab9_10CharpT
{
    public class Lab9T2
    {
        public async Task Run()
        {
            try
            {
                string formula = await File.ReadAllTextAsync("text.txt");
                int result = EvaluateFormula(formula);
                Console.WriteLine($"{formula} = {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private int EvaluateFormula(string formula)
        {
            Stack<int> stack = new Stack<int>();
            for (int i = formula.Length - 1; i >= 0; i--)
            {
                char ch = formula[i];
                if (char.IsDigit(ch))
                {
                    stack.Push(ch - '0');
                }
                else if (ch == 'M' || ch == 'm')
                {
                    int left = stack.Pop();
                    int right = stack.Pop();
                    if (ch == 'M')
                    {
                        stack.Push(Math.Max(left, right));
                    }
                    else if (ch == 'm')
                    {
                        stack.Push(Math.Min(left, right));
                    }
                }
            }
            return stack.Pop();
        }
    }

    public class Employee
    {
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public int Age { get; set; }
        public decimal Salary { get; set; }

        public override string ToString()
        {
            return $"{LastName} {FirstName} {MiddleName} {Gender} {Age} {Salary}";
        }
    }

    public class FormulaEvaluator : IEnumerable, IComparable<FormulaEvaluator>, ICloneable
    {
        private string _formula;

        public FormulaEvaluator(string formula)
        {
            _formula = formula;
        }

        public int Evaluate()
        {
            Stack<int> stack = new Stack<int>();
            for (int i = _formula.Length - 1; i >= 0; i--)
            {
                char ch = _formula[i];
                if (char.IsDigit(ch))
                {
                    stack.Push(ch - '0');
                }
                else if (ch == 'M' || ch == 'm')
                {
                    int left = stack.Pop();
                    int right = stack.Pop();
                    if (ch == 'M')
                    {
                        stack.Push(Math.Max(left, right));
                    }
                    else if (ch == 'm')
                    {
                        stack.Push(Math.Min(left, right));
                    }
                }
            }
            return stack.Pop();
        }

        public IEnumerator GetEnumerator()
        {
            return _formula.GetEnumerator();
        }

        public int CompareTo(FormulaEvaluator other)
        {
            return Evaluate().CompareTo(other.Evaluate());
        }

        public object Clone()
        {
            return new FormulaEvaluator(_formula);
        }

        public override string ToString()
        {
            return _formula;
        }
    }

    public class StaffMember : IComparable<StaffMember>, ICloneable
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public decimal Salary { get; set; }

        public int CompareTo(StaffMember other)
        {
            return Age.CompareTo(other.Age);
        }

        public object Clone()
        {
            return new StaffMember
            {
                LastName = this.LastName,
                FirstName = this.FirstName,
                MiddleName = this.MiddleName,
                Gender = this.Gender,
                Age = this.Age,
                Salary = this.Salary
            };
        }

        public override string ToString()
        {
            return $"{LastName} {FirstName} {MiddleName} {Gender} {Age} {Salary}";
        }
    }

    public class StaffMemberManager : IEnumerable
    {
        private ArrayList staffMembers = new ArrayList();

        public void AddStaffMember(StaffMember staffMember)
        {
            staffMembers.Add(staffMember);
        }

        public IEnumerator GetEnumerator()
        {
            return staffMembers.GetEnumerator();
        }

        public async Task ProcessAndDisplayStaffMembers()
        {
            Queue<StaffMember> youngStaffMembers = new Queue<StaffMember>();
            Queue<StaffMember> otherStaffMembers = new Queue<StaffMember>();

            foreach (StaffMember staffMember in staffMembers)
            {
                if (staffMember.Age < 30)
                {
                    youngStaffMembers.Enqueue(staffMember);
                }
                else
                {
                    otherStaffMembers.Enqueue(staffMember);
                }
            }

            Console.WriteLine("Staff members under 30:");
            while (youngStaffMembers.Count > 0)
            {
                Console.WriteLine(youngStaffMembers.Dequeue());
            }

            Console.WriteLine("\nOther staff members:");
            while (otherStaffMembers.Count > 0)
            {
                Console.WriteLine(otherStaffMembers.Dequeue());
            }
        }
    }

    // Музичний каталог
    public class Song
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public int Duration { get; set; } // Duration in seconds

        public Song(string title, string artist, int duration)
        {
            Title = title;
            Artist = artist;
            Duration = duration;
        }

        public override string ToString()
        {
            return $"{Title} by {Artist}, Duration: {Duration} seconds";
        }
    }

    public class MusicDisk
    {
        public string DiskTitle { get; set; }
        private List<Song> songs;

        public MusicDisk(string diskTitle)
        {
            DiskTitle = diskTitle;
            songs = new List<Song>();
        }

        public void AddSong(Song song)
        {
            songs.Add(song);
        }

        public void RemoveSong(Song song)
        {
            songs.Remove(song);
        }

        public List<Song> Songs
        {
            get { return songs; }
        }

        public void ListSongs()
        {
            Console.WriteLine($"Songs in disk {DiskTitle}:");
            foreach (var song in songs)
            {
                Console.WriteLine(song);
            }
        }
    }

    public class MusicCatalog
    {
        private Hashtable disks;

        public MusicCatalog()
        {
            disks = new Hashtable();
        }

        public void AddDisk(MusicDisk disk)
        {
            if (!disks.ContainsKey(disk.DiskTitle))
            {
                disks.Add(disk.DiskTitle, disk);
            }
        }

        public void RemoveDisk(string diskTitle)
        {
            disks.Remove(diskTitle);
        }

        public void AddSongToDisk(string diskTitle, Song song)
        {
            if (disks.ContainsKey(diskTitle))
            {
                MusicDisk disk = (MusicDisk)disks[diskTitle];
                disk.AddSong(song);
            }
        }

        public void RemoveSongFromDisk(string diskTitle, Song song)
        {
            if (disks.ContainsKey(diskTitle))
            {
                MusicDisk disk = (MusicDisk)disks[diskTitle];
                disk.RemoveSong(song);
            }
        }

        public void ListCatalog()
        {
            Console.WriteLine("Catalog content:");
            foreach (DictionaryEntry entry in disks)
            {
                MusicDisk disk = (MusicDisk)entry.Value;
                Console.WriteLine($"Disk: {disk.DiskTitle}");
                disk.ListSongs();
            }
        }

        public void ListSongsInDisk(string diskTitle)
        {
            if (disks.ContainsKey(diskTitle))
            {
                MusicDisk disk = (MusicDisk)disks[diskTitle];
                disk.ListSongs();
            }
        }

        public void SearchByArtist(string artist)
        {
            Console.WriteLine($"Searching for songs by {artist}:");
            foreach (DictionaryEntry entry in disks)
            {
                MusicDisk disk = (MusicDisk)entry.Value;
                foreach (var song in disk.Songs)
                {
                    if (song.Artist.Equals(artist, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine($"{song} on disk {disk.DiskTitle}");
                    }
                }
            }
        }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            async Task task1()
            {
                Lab9T2 lab9task2 = new Lab9T2();
                await lab9task2.Run();
            }

            async Task task2()
            {
                try
                {
                    string[] lines = await File.ReadAllLinesAsync("employees.txt");
                    Queue<Employee> youngEmployees = new Queue<Employee>();
                    Queue<Employee> otherEmployees = new Queue<Employee>();

                    foreach (string line in lines)
                    {
                        string[] parts = line.Split(' ');
                        if (parts.Length != 6)
                        {
                            Console.WriteLine($"Skipping invalid line: {line}");
                            continue;
                        }

                        Employee employee = new Employee
                        {
                            LastName = parts[0],
                            FirstName = parts[1],
                            MiddleName = parts[2],
                            Gender = parts[3],
                            Age = int.Parse(parts[4]),
                            Salary = decimal.Parse(parts[5])
                        };

                        if (employee.Age < 30)
                        {
                            youngEmployees.Enqueue(employee);
                        }
                        else
                        {
                            otherEmployees.Enqueue(employee);
                        }
                    }

                    Console.WriteLine("Employees under 30:");
                    while (youngEmployees.Count > 0)
                    {
                        Console.WriteLine(youngEmployees.Dequeue());
                    }

                    Console.WriteLine("\nOther employees:");
                    while (otherEmployees.Count > 0)
                    {
                        Console.WriteLine(otherEmployees.Dequeue());
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            async Task task3()
            {
                // Завдання 1
                string formula = "M(m(3,5),M(1,2))";
                FormulaEvaluator evaluator = new FormulaEvaluator(formula);
                int result = evaluator.Evaluate();
                Console.WriteLine($"{formula} = {result}");

                // Завдання 2
                string[] staffMemberData = {
                    "Smith John Doe M 28 3000",
                    "Johnson Jane Doe F 35 4000",
                    "Brown Bob Brown M 24 3500"
                };

                StaffMemberManager staffMemberManager = new StaffMemberManager();

                foreach (string line in staffMemberData)
                {
                    string[] parts = line.Split(' ');
                    if (parts.Length != 6)
                    {
                        Console.WriteLine($"Skipping invalid line: {line}");
                        continue;
                    }

                    StaffMember staffMember = new StaffMember
                    {
                        LastName = parts[0],
                        FirstName = parts[1],
                        MiddleName = parts[2],
                        Gender = parts[3],
                        Age = int.Parse(parts[4]),
                        Salary = decimal.Parse(parts[5])
                    };

                    staffMemberManager.AddStaffMember(staffMember);
                }

                await staffMemberManager.ProcessAndDisplayStaffMembers();
            }

            void task4() 
            {
                MusicCatalog catalog = new MusicCatalog();

                MusicDisk disk1 = new MusicDisk("Best of Rock");
                disk1.AddSong(new Song("Bohemian Rhapsody", "Queen", 354));
                disk1.AddSong(new Song("Stairway to Heaven", "Led Zeppelin", 482));
                
                MusicDisk disk2 = new MusicDisk("Pop Hits");
                disk2.AddSong(new Song("Thriller", "Michael Jackson", 358));
                disk2.AddSong(new Song("Like a Prayer", "Madonna", 331));

                catalog.AddDisk(disk1);
                catalog.AddDisk(disk2);

                catalog.ListCatalog();

                catalog.RemoveSongFromDisk("Best of Rock", new Song("Bohemian Rhapsody", "Queen", 354));
                catalog.ListSongsInDisk("Best of Rock");

                catalog.SearchByArtist("Madonna");
            }

            while (true)
            {
                Console.WriteLine("  ****  Lab 9  ****  \n\n");
                Console.Write("Press 0 to exit\n");
                Console.Write("Which task would you like to review? (1-4): ");
                string? str = Console.ReadLine();
                if (str == "0") break;
                if (str != null && short.TryParse(str, out short ans))
                {
                    switch (ans)
                    {
                        case 1:
                            await task1();
                            break;
                        case 2:
                            await task2();
                            break;
                        case 3:
                            await task3();
                            break;
                        case 4:
                            task4();
                            break;
                        default:
                            Console.WriteLine("Put the correct number");
                            break;
                    }
                }
            }
        }
    }
}
