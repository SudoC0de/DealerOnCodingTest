using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;

namespace MarsRovers
{
    class Rover
    {
        private char[] _commands = new char[3] { 'L', 'R', 'M' };
        private int _roverXCurrentPos = 0;
        private int _roverYCurrentPos = 0;
        private char _roverCurrentHeading = 'N';

        private int CurrentXPos
        {
            get
            {
                return _roverXCurrentPos;
            }
            set
            {
                _roverXCurrentPos = value;
            }
        }

        private int CurrentYPos
        {
            get
            {
                return _roverYCurrentPos;
            }
            set
            {
                _roverYCurrentPos = value;
            }
        }

        private char CurrentHeading
        {
            get
            {
                return _roverCurrentHeading;
            }
            set
            {
                _roverCurrentHeading = value;
            }
        }

        public Rover()
        {
        }

        public void PilotRover()
        {
            Plateau plateau = new Plateau();

            DeterminePlateauBounds(plateau);
            SetRoverLandingPoint();
            DriveRover(plateau);
        }

        private void DeterminePlateauBounds(Plateau p)
        {
            SetNorthernPlateauBounds(p);
            SetEasternPlateauBounds(p);
        }

        private void SetNorthernPlateauBounds(Plateau p)
        {
            bool validBounds = false;

            while (validBounds == false)
            {
                Console.WriteLine("What is the northern bounds of the Mars plateau? ");

                string plateauNorthernBounds = Console.ReadLine().Replace(" ", string.Empty);

                try
                {
                    int northernBounds = int.Parse(plateauNorthernBounds);

                    if (northernBounds >= 1)
                    {
                        p.NorthBounds = northernBounds;
                        validBounds = true;
                        Console.Clear();
                    }
                    else
                    {
                        Console.WriteLine("Northern bounds cannot be less than 1.");
                        Console.ReadLine();
                        Console.Clear();
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Northern bounds does not have only integer values.");
                    Console.ReadLine();
                    Console.Clear();
                }
                catch (OverflowException)
                {
                    Console.WriteLine(string.Format("Northern bounds cannot be greater than {0}.", int.MaxValue));
                    Console.ReadLine();
                    Console.Clear();
                }
                catch (Exception e)
                {
                    Console.WriteLine(string.Format("Uncaught Exception: {0}", e));
                    Console.ReadLine();
                    Console.Clear();
                }
            }
        }

        private void SetEasternPlateauBounds(Plateau p)
        {
            bool validBounds = false;

            while (validBounds == false)
            {
                Console.WriteLine("What is the eastern bounds of the Mars plateau? ");

                string plateauEasternBounds = Console.ReadLine().Replace(" ", string.Empty);

                try
                {
                    int easternBounds = int.Parse(plateauEasternBounds);
                        
                    if (easternBounds >= 1)
                    {
                        p.EastBounds = easternBounds;
                        validBounds = true;
                        Console.Clear();
                    }
                    else
                    {
                        Console.WriteLine("Eastern bounds cannot be less than 1.");
                        Console.ReadLine();
                        Console.Clear();
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Eastern bounds does not have only integer values.");
                    Console.ReadLine();
                    Console.Clear();
                }
                catch (OverflowException)
                {
                    Console.WriteLine(string.Format("Eastern bounds cannot be greater than {0}.", int.MaxValue));
                    Console.ReadLine();
                    Console.Clear();
                }
                catch (Exception e)
                {
                    Console.WriteLine(string.Format("Uncaught Exception: {0}", e));
                    Console.ReadLine();
                    Console.Clear();
                }
            }
        }

        private void SetRoverLandingPoint()
        {
            CurrentXPos = 1;
            CurrentYPos = 2;
            CurrentHeading = 'N';
        }

        private void DriveRover(Plateau p)
        {
            bool isDriving = true;

            Console.WriteLine(string.Format("Plateau Bounds: North = {0}, East = {1}, South = {2}, West = {3}", p.NorthBounds, p.EastBounds, p.SouthBounds, p.WestBounds));

            while (isDriving == true)
            {
                Console.WriteLine(string.Format("Current Rover Location & Heading: ({0},{1}) {2}", CurrentXPos, CurrentYPos, CurrentHeading));
                Console.WriteLine(string.Format("Enter Driving Commands (L,R,M):"));

                string drivingCommand = Console.ReadLine().Replace(" ", string.Empty);

                if (IsValidDrivingCommand(drivingCommand))
                {
                    SetRoverPath(p, drivingCommand);

                    bool roverValidResponse = false;

                    string pilotInput = "Y";

                    while (roverValidResponse == false)
                    {
                        Console.WriteLine(string.Format("Current Rover Location & Heading: ({0},{1}) {2}", CurrentXPos, CurrentYPos, CurrentHeading));
                        Console.WriteLine("Do you want to continue driving the Mars Rover? (Y/N)");
                        pilotInput = Console.ReadLine().Replace(" ", string.Empty);

                        if (pilotInput != "Y" && pilotInput != "y" && pilotInput != "N" && pilotInput != "n")
                        {
                            Console.WriteLine("Answer must be Y or N");
                            continue;
                        }
                        else
                        {
                            Console.Clear();
                            roverValidResponse = true;
                        }
                    }

                    if (pilotInput == "N" || pilotInput == "n")
                    {
                        isDriving = false;
                    }
                }
                else
                {
                    Console.ReadLine();
                    Console.Clear();
                }
            }
        }

        private bool IsValidDrivingCommand(string command)
        {
            foreach (char c in command)
            {
                if (c != 'L' && c != 'l' && c != 'R' && c != 'r' && c != 'M' && c != 'm')
                {
                     Console.WriteLine("Driving command doesn't include only: L, R, or M");
                    return false;
                }
            }

            return true;
        }

        private void SetRoverPath(Plateau p, string command)
        {
            foreach(char c in command)
            {
                if (c.ToString().ToUpper() == "L")
                {
                    ChangeRoverDirection(c);
                }
                else if (c.ToString().ToUpper() == "R")
                {
                    ChangeRoverDirection(c);
                }
                else if (c.ToString().ToUpper() == "M")
                {
                    MoveRover();

                    if (!CheckPlateauBounds(p))
                    {
                        Console.WriteLine(string.Format("Rover hit the edge of the plateau and stopped at location ({0},{1})", CurrentXPos, CurrentYPos));
                        Console.ReadLine();
                        break;
                    }
                }
            }
        }

        private void ChangeRoverDirection(char d)
        {
            if (d.ToString().ToUpper() == "L")
            {
                switch (CurrentHeading)
                {
                    case 'N':
                        CurrentHeading = 'W';
                        break;
                    case 'E':
                        CurrentHeading = 'N';
                        break;
                    case 'S':
                        CurrentHeading = 'E';
                        break;
                    case 'W':
                        CurrentHeading = 'S';
                        break;
                }
            }
            else if (d.ToString().ToUpper() == "R")
            {
                switch (CurrentHeading)
                {
                    case 'N':
                        CurrentHeading = 'E';
                        break;
                    case 'E':
                        CurrentHeading = 'S';
                        break;
                    case 'S':
                        CurrentHeading = 'W';
                        break;
                    case 'W':
                        CurrentHeading = 'N';
                        break;
                }
            }
        }

        private void MoveRover()
        {
            switch (CurrentHeading)
            {
                case 'N':
                    CurrentYPos++;
                    break;
                case 'E':
                    CurrentXPos++;
                    break;
                case 'S':
                    CurrentYPos--;
                    break;
                case 'W':
                    CurrentXPos--;
                    break;
            }
        }

        private bool CheckPlateauBounds(Plateau p)
        {
            if (CurrentXPos < p.WestBounds)
            {
                CurrentXPos = p.WestBounds;

                return false;
            }
            else if (CurrentXPos > p.EastBounds)
            {
                CurrentXPos = p.EastBounds;

                return false;
            }
            else if (CurrentYPos < p.SouthBounds)
            {
                CurrentYPos = p.SouthBounds;

                return false;
            }
            else if (CurrentYPos > p.NorthBounds)
            {
                CurrentYPos = p.NorthBounds;

                return false;
            }

            return true;
        }
    }
}