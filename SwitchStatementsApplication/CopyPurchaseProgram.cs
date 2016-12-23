using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchStatementsApplication
//This program creates a simple thing, which enables
//people to buy coffee, and ultimately presents a bill.
{
    class CopyPurchaseProgram
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Subrean's Coffee Shop!\n");
            int totalCostOfCoffee = 0;
            //For switch statements like these that involve math of some sort,
            //always put your math variables before the first label, like put
            //it over here, for instance. 
        start:
            //This is a label called start.
            //It's a marker of sorts.
            //Labels essentially are, where, say you want
            //to do something again at the goto thing.
            //The program goes back up to Start, which
            //is here, in this case, and the program runs
            //again.
            Console.WriteLine("Choose size of coffee: 1 - Small, 2 - Medium, 3 - Large");
            //This presents a menu to the customer, asking
            //the customer to pick the size of coffee they
            //want.
            int coffeeSize = int.Parse(Console.ReadLine());            

            //In our coffee shop, a small coffe costs $1.00.
            //Medium coffee is $2, and large coffee is $3.
            //We have to calculate the costs accordingly.

            switch (coffeeSize)
            {
                case 1:
                //If the customer selects 1, a small coffee
                    totalCostOfCoffee += 1;
                    //Adds 1.00 to totalCostOfCoffee ($1.00)
                    break;
                case 2://Medium
                //Another thing to note:
                //Since coffeSize is an int, the cases are int as well,
                //as the cases have to respect that coffeeSize is an int.
                //So, for this switch statement at least, the cases are int.
                    totalCostOfCoffee += 2;
                    //goto case 1;
                    //You can actually even do this too, with goto.
                    break;
                case 3://Large
                    totalCostOfCoffee += 3;
                    break;
                default:
                    Console.WriteLine("Invalid coffee size!");
                    goto start;
                    //This takes you back to start, if you entered an invalid
                    //coffee size. From there, the program runs again to
                    //prompt you to enter a valid coffee size of 1, 2, or 3.
            }

            yesOrNo:
            //This is another marker.
            //If the user enters something other than yes or no, then it's invalid,
            //and the program starts from here again to prompt the customer to
            //enter a valid choice of yes or no.
            Console.WriteLine("Do you want to buy anything else? - Yes or No?\n");
            //This asks the user, if they want to order more.
            //If yes,
            //If no,
            string continueOrdering = Console.ReadLine();

            switch (continueOrdering)
            {
                case "Yes":
                case "YES":
                //Here, this time, the cases are all string types,
                //since continueOrdering is a string.
                case "yes":
                //I had this bascially setup, so that if the
                //customer enters either 'Yes', 'YES' or 'yes', then
                //it is read. I've made it not case sensitive.
                    goto start;
                //Here, this is a goto being used.
                //What this is basically saying is that, if the customer
                //inputs yes, then the program will back to where start is,
                //and run the switch statement coffeeSize again, so that they
                //can go order another coffee.
                //Also, you don't need to use a break for the goto stuff.
                //If you use a break, you'll get a green
                //squiggly saying unreachable code detected.
                //What that means is that the code will not
                //execute.
                //For goto, once the case here reaches goto,
                //the program goes right back up to the label
                //start in this case, and the break statement
                //is never reached.
                //break;
                case "No":
                case "NO":
                case "no":
                    break;
                default:
                    Console.WriteLine("Your choice, {0}, is invalid. Please enter a valid choice.", continueOrdering);
                    //The {0} placeholder can also show strings as well. It doesn't have to
                    //necessarily be a number everytime.
                    goto yesOrNo;
                    //This runs, if the customer entered something other than yes or no.
                    //It runs the switch statement continueOrdering again to prompt the
                    //customer to enter a valid choice of yes or no.
            }

            /*switch (continueOrdering.ToUpper())
            //ToUpper() converts all the characters in
            //the string, continueOrdering, into uppercase
            //characters.
            //So now, no matter what the user enters, it
            //is all converted to upper case letters.
            //You can do the same thing with ToLower().
            //ToLower() basically is like ToUpper(), only
            //it's lowercase this time around.
            //ToUpper and ToLower() methods are examples
            //of string manipulation.
            {
                case "YES":
                    goto start;
                case "NO":
                    break;
                default:
                    Console.WriteLine("Your choice, {0}, is invalid. Please enter a valid choice.", continueOrdering);
                    goto yesOrNo;
            }*/

            Console.WriteLine("Thank you for shopping with us at Subrean's Coffee Shop!\nEnjoy your coffee!\n");
            Console.WriteLine("The total cost of your purchase is ${0}.", totalCostOfCoffee);
            Console.ReadKey();
            //Note: Using goto is bad programming style. You should avoid
            //using goto by all means.
            //It's because, if one program jumps randomly, from one location
            //to another, debugging that program will be quite complex, and
            //anything that you can achieve with goto can be achieved with
            //loops. So yeah, simply, you don't really want to use goto.
        }
    }
}
