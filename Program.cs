using System;

internal class Program
{
    static void Main(string[] args)
    {
        bool continueProgram = true;

        while (continueProgram)
        {
            string NameCustomer = NameCustom();
            double consumption = WaterMeter();
            ShowMenu();
            int CustomerType = Choice();
            double Bill = CustomerRate(CustomerType, consumption);
            double environmentalFee = Bill * 0.10;
            double vat = (Bill + environmentalFee) * 0.10;
            double TotalBill = Bill + environmentalFee + vat;
            Console.WriteLine("Customer Name: " + NameCustomer);
            Console.WriteLine("Consumption: " + consumption + "m3");
            Console.WriteLine("TotalBill: " + TotalBill);
            Console.WriteLine("VAT: " + vat + "VND");
            Console.WriteLine("environmentFee: " + environmentalFee + "VND");

            continueProgram = AskContinue();
        }
    }

    static string NameCustom()
    {
        Console.WriteLine("Enter name: ");
        string NameCustomer = Console.ReadLine();
        return NameCustomer;
    }

    static double WaterMeter()
    {
        double LastMonth, ThisMonth;
        do
        {
            Console.WriteLine("Last month's water meter reading: ");

            while (!double.TryParse(Console.ReadLine(), out LastMonth))
            {
                Console.WriteLine("Input incorrect. Please try again!!");
            }

            Console.WriteLine("This month's water meter reading: ");

            while (!double.TryParse(Console.ReadLine(), out ThisMonth))
            {
                Console.WriteLine("Input incorrect. Please try again!!");
            }

            if (LastMonth > ThisMonth)
            {
                Console.WriteLine("Error! Please enter LastMonth again: ");
            }
        } while (LastMonth > ThisMonth);

        Console.Clear();
        double consumption = ThisMonth - LastMonth;
        return consumption;
    }

    static void ShowMenu()
    {
        Console.WriteLine("------CustomerType------");
        Console.WriteLine("1. Household.");
        Console.WriteLine("2. Government administrative agency, public services.");
        Console.WriteLine("3. Production unit.");
        Console.WriteLine("4. Business services.");
    }

    static int Choice()
    {
        int CustomerType;
        while (true)
        {
            CustomerType = Convert.ToInt32(Console.ReadLine());
            if (CustomerType >= 1 && CustomerType <= 4)
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid, please re-enter: ");
            }
        }
        return CustomerType;
    }

    static double CustomerRate(int CustomerType, double consumption)
    {
        double rate = 0;
        switch (CustomerType)
        {
            case 1:
                Console.WriteLine("Household");
                Console.Write("Enter AmountOfPeople: ");
                int AmountOfPeople = Convert.ToInt32(Console.ReadLine());

                if (consumption > 0 && consumption <= 10)
                {
                    rate = 5.973;
                }
                else if (consumption > 10 && consumption <= 20)
                {
                    rate = 7.052;
                }
                else if (consumption > 20 && consumption <= 30)
                {
                    rate = 8.669;
                }
                else
                {
                    rate = 15.929;
                }
                break;
            case 2:
                Console.WriteLine("Government administrative agency, public services");
                rate = 9.955;
                break;
            case 3:
                Console.WriteLine("Production unit");
                rate = 11.615;
                break;
            case 4:
                Console.WriteLine("Business services");
                rate = 22.068;
                break;
            default:
                Console.WriteLine("Invalid input");
                Environment.Exit(0);
                break;
        }

        double[] rates = { 5.973, 7.052, 8.699, 15.929 };
        double[] limits = { 10, 20, 30 };
        double Bill = 0;

        if (CustomerType == 1)
        {
            if (consumption <= limits[0])
            {
                Bill = consumption * rates[0];
            }
            else if (consumption <= limits[1])
            {
                Bill = limits[0] * rates[0] +
                        (consumption - limits[0]) * rates[1];
            }
            else if (consumption <= limits[2])
            {
                Bill = limits[0] * rates[0] +
                        (limits[1] - limits[0]) * rates[1] +
                        (consumption - limits[1]) * rates[2];
            }
            else
            {
                Bill = limits[0] * rates[0] +
                        (limits[1] - limits[0]) * rates[1] +
                        (limits[2] - limits[1]) * rates[2] +
                        (consumption - limits[2]) * rates[3];
            }
        }
        else
        {
            Bill = consumption * rate;
        }

        return Bill;
    }

    static bool AskContinue()
    {
        Console.WriteLine("Do you want to calculate another bill? (yes/no)");
        string response = Console.ReadLine().Trim().ToLower();
        return response == "yes";
    }
}