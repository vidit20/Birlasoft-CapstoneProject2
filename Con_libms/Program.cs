using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Con_libms
{
    public class LibraryBooks
    {
        public int bksBorrowed;                         // keeping details of no. of books borrowed
        public object[,] book = new object[5, 2];       //It keeps the details of books in the catalogue(name & no. of copies)
        public object[,] bookIssue = new object[3, 2]; //for a particular user (name & issue date)
        public virtual void catalogue()                //to manipulate books
        {
            System.Console.WriteLine("Librarian's Catalogue Management");
        }
        public LibraryBooks()                          //public constructor - create books
        {
            char va = 'a';                             // declare variable 
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 1; j++)
                {
                    book[i, j] = va;
                    va++;                              // incrementing variable
                }
                for (int j = 1; j < 2; j++)
                {
                    book[i, j] = 5;
                }
            }
        }
    }
    public interface Lib        //creating interface
    {
        bool searchB(string s); // Searing book in borrower class
        void borrow(String bor, DateTime t);  // borrowing book in borrower class
        void returnBook(string s);  // returning book in borrower class
        void Details();
    }

    //represents the Borrower
    class borrower : LibraryBooks , Lib 
    {
        public borrower() : base()   
        {
            bksBorrowed = 0;
            for(int i=0; i < 3; i++)
            {
                for (int j=0; j < 2; j++)
                {
                    bookIssue[i, j] = 0; //for a particular user(name & issue date)
                }
            }
        }
        public bool searchB(string s)  //parametrized function - here parameter is holds the name of the book
        {
            bool a = true;
            for( int i=0; i < 5; i++)
            {
                for(int j=0; j < 1; j++)
                { string ff = book[i, j].ToString();
                if(ff.Equals(s))
                    {
                        a= true;
                        return a;
                    }
                else
                        a = false;
                }
            }
            return a;
        }
        //it takes book borrow information 
        public void borrow(string bor, DateTime t)
        {
            if (bor.Equals("b"))
            {
                Console.WriteLine("You cannot borrow this journal, It is for reference only!");
                return;
            }
            if(this.bksBorrowed < 5)
            {
                bookIssue[this.bksBorrowed, 0] = bor;
                bookIssue[this.bksBorrowed, 1] = t;
                this.bksBorrowed++;
            }
            else
            {
                Console.WriteLine("Sorry! you are not eligible !!");
            }
        }
        public void returnBook(string s)
        {
            for(int i=0; i < 5; i++)
            {
                for (int j=0; j < 1; j++)
                {
                    if (book[i, j].Equals(s))
                    {
                        book[i, j + 1] = (int)book[i, j + 1] + 1;
                    }
                }
            }
            for (int i=0; i < this.bksBorrowed; i++)
            {
                for(int j = 0; j < 1; j++)
                {
                    if (this.bookIssue[i, j].Equals(s))
                    {
                        TimeSpan? f;
                        f = DateTime.Today - (DateTime)this.bookIssue[i, j + 1];
                        if(f?.TotalDays > 15)
                        {
                            Console.WriteLine("");// penalty calcualtion
                            Console.WriteLine("Your penalty is" + f?.TotalDays * 5);
                            Console.WriteLine("");
                        }
                    }
                }
            }
            bksBorrowed--;
        }
        // displaying details of the book
        public void Details()
        {
            Console.WriteLine("Total Books You'd borrowed till now {0} \n" + "Name" + "   " + "Issue Date", this.bksBorrowed + 0);
            for(int i=0; i < this.bksBorrowed; i++)
            {
                for(int j = 0; j < 2; j++)
                {
                    Console.Write(this.bookIssue[i, j] + "  "+"  ");
                }
                Console.WriteLine();
            }
        }
        // renewing book function
         public void renew(string k)
        {
            for(int i = 0; i < this.bksBorrowed; i++)
            {
                for(int j=0; j < 1; j++)
                {
                    if (bookIssue[i, j].Equals(k))
                    {
                        bookIssue[i, j + 1] = DateTime.Today;
                    }
                }
            }
            Console.WriteLine("Book has been renewed!");
        }
    }
    //represents the Librarian
    sealed class Librarian : LibraryBooks
    {
        public int borNo;
        public Librarian()  //public constructor
        {
            borNo = 0;
        }
        String[,] fname = new string[5, 2];
        public void createborrower(String name, String pwd)
        {
            for (int i = 0; i <= this.borNo; i++)
            {
                for (int j = 0; j < 1; j++)
                {
                    this.fname[i, j] = name;
                }
                for (int j = 1; j < 2; j++)
                {
                    this.fname[i, j] = pwd;
                }
            }
        }
        public int borchk(string n, string p) // for authentication
        {
            int dla = 0;
            if (this.borNo == 0)
            {
                return 2;
            }
            else
            {
                for (int i = 0; i <= this.borNo; i++)
                {
                    for (int j = 0; j < 1; j++)
                    {
                        string ko = this.fname[i, j];
                        if (ko.Equals(n))
                        {
                            dla = 1;
                            if (this.fname[i, j + 1].Equals(p))
                            {
                                return 1;
                            }
                        }
                    }
                }
            }
            if (dla == 0)
                return 2;
            else
                return 0;
        }
        public void viewBor()
        {
            Console.WriteLine("Name" + "       " + "Password");
            for (int i = 0; i < this.borNo; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    Console.Write(this.fname[i, j] + "          ");
                }
                Console.WriteLine("  ");
            }
        }
        // menu for librarian
        public override void catalogue()
        {
            String ans = "yes";

            do
            {
                Console.WriteLine("--------------------------------");
                Console.WriteLine("| 1. View Books                |");
                Console.WriteLine("| 2. Add Books                 |");
                Console.WriteLine("| 3. Delete Books              |");
                Console.WriteLine("| 4. Return to main menu       |");
                Console.WriteLine("| Please Enter your choice     |");
                Console.WriteLine("|------------------------------|");
                Console.WriteLine("");
                int n = int.Parse(Console.ReadLine());  //Convert string to integer 
                switch (n)
                {
                    case 1://View Books
                        Console.WriteLine("========================================");
                        Console.WriteLine("Book name " + "    " + "Available copies");
                        Console.WriteLine("========================================");
                        for (int i = 0; i < 5; i++)
                        {
                            for (int j = 0; j < 2; j++)
                            {
                                Console.Write(this.book[i, j] + "                   ");
                            }
                            Console.WriteLine("");
                        }
                        break;
                    case 2: //Add Books
                        Console.WriteLine("=======================================");
                        Console.WriteLine("Enter the name of the books");
                        string sn = Console.ReadLine();
                        Console.WriteLine("========================================");
                        Console.WriteLine("Enter the no. of copies you want to add");
                        int ner = int.Parse(Console.ReadLine());
                        Console.WriteLine("========================================");
                        for (int i = 0; i < 5; i++)
                        {
                            for (int j = 0; j < 1; j++)
                            {
                                string sd = book[i, j].ToString();
                                if (sd.Equals(sn))
                                {
                                    book[i, j + 1] = (int)book[i, j + 1] + ner;
                                    break;
                                }
                            }
                        }
                        break;
                    case 3: //Delete Books
                        Console.WriteLine("==========================================");
                        Console.WriteLine("Enter the name of the books");
                        string sn1 = Console.ReadLine();
                        Console.WriteLine("==========================================");
                        Console.WriteLine("Enter the no. of copies you want to remove");
                        int n2 = int.Parse(Console.ReadLine());
                        Console.WriteLine("==========================================");
                        for (int i = 0; i < 5; i++)
                        {
                            for (int j = 0; j < 1; j++)
                            {
                                string sd = book[i, j].ToString();
                                if (sd.Equals(sn1));
                                {
                                    book[i, j + 1] = (int)book[i, j + 1] - n;
                                    break;
                                }
                            }
                        }
                        break;
                    case 4: //Return to main menu 
                        return;
                }
            } while (ans.Equals("yes"));
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string ans = "yes";
            Librarian lirar = new Librarian();  //create a object 
            do
            {
                Console.WriteLine("");
                Console.WriteLine("**--------------------------------------------**");
                Console.WriteLine("|Welcome to library management system          |");
                Console.WriteLine("|Please login into any of the following account|");
                Console.WriteLine("|1.Librarian                                   |");
                Console.WriteLine("|2.Borrower                                    |");
                Console.WriteLine("|3.Exit                                        |");
                Console.WriteLine("**--------------------------------------------**");
                int ch = int.Parse(Console.ReadLine());
                Program p = new Program();
                switch (ch)
                {
                    case 1: //Librarian
                        p.adm(lirar);
                        break;
                    case 2: //Borrower
                        Console.WriteLine("Please enter your name");
                        String name = Console.ReadLine();
                        Console.WriteLine("Please enter your password");
                        string pwd = Console.ReadLine();
                        int w = lirar.borchk(name, pwd);
                        if (w == 1)
                            p.borr(lirar);
                        else if (w == 2)
                        {
                            Console.WriteLine("Account Created!!");
                            Console.WriteLine("Account Login!!");
                            lirar.createborrower(name, pwd);
                            p.borr(lirar);
                        }
                        else
                            Console.WriteLine("Wrong Password or username!");
                        break;
                    case 3: //Exit
                        return;
                        break;
                    default:
                        Console.WriteLine("Please enter a valid choice");
                        break;
                }
                Console.WriteLine("========================================");
                Console.WriteLine("Do you want to continue?");
                ans = Console.ReadLine();
                Console.WriteLine("========================================");
            }
            while (ans.Equals("yes"));
        }
        public void borr(Librarian lirar)
        {
            string ans = "yes";
            borrower bo = new borrower(); //create object
            lirar.borNo += 1;

            do
            {
                Console.WriteLine("**-------------------------------------**");
                Console.WriteLine("|Please make a choice from the following|");
                Console.WriteLine("|1.Search Books                         |");
                Console.WriteLine("|2.Return Books                         |");
                Console.WriteLine("|3.Borrow Books                         |");
                Console.WriteLine("|4.Renew Books                          |");
                Console.WriteLine("|5.View details                         |");
                Console.WriteLine("|6.Return to main menu                  |");
                Console.WriteLine("**-------------------------------------**");
                Console.WriteLine("");
                int ch = int.Parse(Console.ReadLine());
                switch (ch)
                {
                    case 1://Search Books
                        Console.WriteLine("========================================");
                        Console.WriteLine("Enter the name of the book");
                        string name = Console.ReadLine();
                        Console.WriteLine("========================================");
                        bool p = bo.searchB(name);
                        if (p == true)
                            Console.WriteLine("Book Found");
                        else
                            Console.WriteLine("Book Not Found");
                        break;
                    case 2: //Return Books 
                        Console.WriteLine("========================================");
                        Console.WriteLine("Enter the book you want to return");
                        String g = Console.ReadLine();
                        Console.WriteLine("========================================");
                        bo.returnBook(g);
                        break;
                    case 3: //Borrow Books
                        Console.WriteLine("========================================");
                        Console.WriteLine("Enter name of the book");
                        string y = Console.ReadLine();
                        Console.WriteLine("========================================");
                        Console.WriteLine("Enter the issue date");
                        DateTime t = Convert.ToDateTime(Console.ReadLine());
                        Console.WriteLine("========================================");
                        bo.borrow(y, t);
                        break;
                    case 4: //Renew Books
                        Console.WriteLine("========================================");
                        Console.WriteLine("Enter the book to renew");
                        String k = Console.ReadLine();
                        Console.WriteLine("========================================");
                        bo.renew(k);
                        break;
                    case 5: //view Book issue details
                        bo.Details();
                        break;
                    case 6: //Return to main menu  
                        return;
                }
                Console.WriteLine("========================================");
                Console.WriteLine("Do you want to Continue?");
                ans = Console.ReadLine();
                Console.WriteLine("========================================");
            }
            while (ans.Equals("yes"));
        }
        public void adm(Librarian lirar)
        {
            String ans = "yes";
            do
            {
                Console.WriteLine("**-------------------------------------**");
                Console.WriteLine("|1.Manage Borrower                      |");
                Console.WriteLine("|2.Maintain Books                       |");
                Console.WriteLine("|3.Return to main menu                  |");
                Console.WriteLine("**-------------------------------------**");
                Console.WriteLine("Please enter your choice");
                //Console.WriteLine("");
                Console.WriteLine("");
                int ch = int.Parse(Console.ReadLine());
                switch (ch)
                {
                    case 1:
                        lirar.viewBor();
                        break;
                    case 2:
                        lirar.catalogue();
                        break;
                    case 3:
                        return;
                }
                Console.WriteLine("========================================");
                Console.WriteLine("Do you want to continue??");
                ans = Console.ReadLine();
                Console.WriteLine("========================================");
            } while (ans.Equals("yes"));
        }
        }
            }