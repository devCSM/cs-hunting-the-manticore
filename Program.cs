using System;

// game start state


// get manticore's distance from city and clear the screen


// Run until Manticore or city score = 0


// Display round number, city health, and manticore health


// compute amount of damage cannon will cause
// default = 1 point, multiple of 3 && 5 = 10 points, multiple of 3 || 5 = 3 points


// get range from second player and resolve its effect
// OVERSHOT, FELL SHORT, DIRECT HIT!


// if manticore is still alive, inflict damage on city


// advance round


// end game if city || manticore health == 0


// different message types = different colors


int cityHealth = 15;
int manticoreHealth = 10;
int round = 1;
int guessedLocation;
int cannonDamage = 0;
bool manticoreHit = false;
bool cityWon = false;
string winner = "The Manticore has been destoryed! The city of Consolas has been saved!";
int manticoreLocation = GetUserInput("Player 1, how far away from the city do you want to station the Manticore?");


ConsoleColor red = ConsoleColor.Red;
ConsoleColor blue = ConsoleColor.Blue;
ConsoleColor white = ConsoleColor.White;
ConsoleColor yellow = ConsoleColor.Yellow;
ConsoleColor black = ConsoleColor.Black;
ConsoleColor green = ConsoleColor.Green;

Console.Clear();

SetTextNormal();

Console.WriteLine("Player 2, it is your turn.");

do
{
    ShowStatus(round, cityHealth, manticoreHealth);
    cannonDamage = GetCannonDamage(round);
    ShowCannonDamage(cannonDamage);
    SetTextNormal();
    guessedLocation = GetUserInput("Enter desired cannon range: ");
    manticoreHit = GetCannonEffect(manticoreLocation, guessedLocation);

    if (manticoreHit) manticoreHealth -= cannonDamage;
    else cityHealth--;

    round++;

} while (cityHealth > 0 && manticoreHealth > 0);


cityWon = cityHealth > 0;

if (!cityWon)
{
    SetTextBad();
    winner = "The Manticore won. The city of Consolas has been destroyed.";
}
else
{ 
    SetTextGood(); 
}

Console.Write(winner);
SetTextNormal();

int GetUserInput(string message)
{
    while (true)
    {
        Console.Write($"{message} ");
        string stringValue = Console.ReadLine();
        int intValue = Convert.ToInt32(stringValue);

        if (intValue >= 0 && intValue <= 100) return intValue;
        else Console.WriteLine("ERROR: Must be between 0 and 100.");
    }
}

void ShowStatus(int round, int cityHealth, int manticoreHealth)
{
    string dashedLine = "-------------------------------------------\n";
    Console.Write(dashedLine);
    SetTextNormal();
    string status = $"STATUS: Round: {round}  " +
        $"City: {cityHealth}/15  " +
        $"Manticore: {manticoreHealth}/10";

    Console.WriteLine(status);
}

int GetCannonDamage(int turn)
{
    bool combo = (turn % 3 == 0 && turn % 5 == 0);
    bool fire = (turn % 3 == 0 && !combo);
    bool electric = (turn % 5 ==0 && !combo);
    int cannonDamage = 0;

    if (combo)
    {
        cannonDamage = 10;
        SetTextCombo();
    }
    else if (fire)
    {
        cannonDamage = 3;
        SetTextFire();
    }
    else if (electric)
    {
        cannonDamage = 3;
        SetTextElectric();
    }
    else
    {
        cannonDamage = 1;
        SetTextNormal();
    }

    return cannonDamage;
}

void ShowCannonDamage(int cannonDamage)
{
    Console.WriteLine($"The cannon is expected to deal {cannonDamage} damage this round.");
}

bool GetCannonEffect(int manticoreLocation, int guessedLocation)
{
    bool overShot = guessedLocation > manticoreLocation;
    bool fellShort = guessedLocation < manticoreLocation;
    bool directHit = guessedLocation == manticoreLocation;

    string message;

    if (overShot)
    {
        message = "OVERSHOT the target.";
        SetTextWarn();
    }
    else if (fellShort)
    {
        message = "FELL SHORT of the target.";
        SetTextWarn();
    }
    else if (directHit)
    {
        message = "was a DIRECT HIT!";
        SetTextGood();
    }
    else
    {
        message = "ERROR: Unable to calculate distance from target.";
        SetTextBad();
    }

    Console.WriteLine($"That round {message}");
    SetTextNormal();

    return directHit;
}



void SetTextNormal()
{
    Console.BackgroundColor = black;
    Console.ForegroundColor = white;
}

void SetTextWarn()
{
    Console.BackgroundColor = yellow;
    Console.ForegroundColor = black;
}

void SetTextFire()
{
    Console.BackgroundColor = black;
    Console.ForegroundColor = red;
}

void SetTextElectric()
{
    Console.BackgroundColor = black;
    Console.ForegroundColor = yellow;
}

void SetTextCombo()
{
    Console.BackgroundColor = black;
    Console.ForegroundColor = blue;
}

void SetTextBad()
{
    Console.BackgroundColor = red;
    Console.ForegroundColor = white;
}

void SetTextGood()
{
    Console.BackgroundColor = green;
    Console.ForegroundColor = white;
}
