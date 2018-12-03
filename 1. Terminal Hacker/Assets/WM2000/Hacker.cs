using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour {

    //Game Config data
    string greeting = "Hello Finn";
    string[] level1Passwords = { "books", "aisle", "self", "password", "font", "borrow" };
    string[] level2Passwords = { "prisoner", "handcuffs", "holster", "uniform", "arrest" };
    string[] level3Passwords = { "books", "aisle", "self", "password", "font", "borrow" };

    //Game State
    int level;
    string password;
    enum Screen { MainMenu, Password, Win };
    Screen currentScreen = Screen.MainMenu;


    // Use this for initialization
    void Start() {
        ShowMainMenu();
    }

    //Show Text Displayed on the main menu
    void ShowMainMenu()
    {
        currentScreen = Screen.MainMenu;
        Terminal.ClearScreen();

        Terminal.WriteLine(greeting);
        Terminal.WriteLine("What would you like to hack into?");
        Terminal.WriteLine("Press 1 for local library");
        Terminal.WriteLine("Press 2 for police station");
        Terminal.WriteLine("Press 3 for NASA");
        Terminal.WriteLine("Enter your selection: ");
    }

    //OnUserInput is a message when the user does input
    void OnUserInput(string input)
    {
        if (input == "menu") //user should always be able to go to main menu
        {
            ShowMainMenu();
        }
        else if (currentScreen == Screen.MainMenu)
        {
            RunMainMenu(input);
        }
        else if (currentScreen == Screen.Password)
        {
            RunPasswordScreen(input);
        }
    }

    private void RunMainMenu(string input)
    {
        bool isValidLevelNumber = (input == "1" || input == "2" || input == "3");
        if (isValidLevelNumber)
        {
            level = int.Parse(input);
            StartGame();
        }
        else if(input == "menu")
        {
            ShowMainMenu();
        }
        else
        {
            Terminal.WriteLine("Please enter a valid input");
        }
    }

    void RunPasswordScreen(string input)
    {
        CheckPassword(input);
    }

    // when 1 2 or 3 is entered
    private void StartGame()
    {
        currentScreen = Screen.Password;
        switch (level)
        {
            case 1:
                password = level1Passwords[UnityEngine.Random.Range(0, level1Passwords.Length)];
                break;
            case 2:
                password = level2Passwords[UnityEngine.Random.Range(0, level2Passwords.Length)];
                break;
            case 3:
                password = level3Passwords[UnityEngine.Random.Range(0, level3Passwords.Length)];
                break;
            default:
                Debug.LogError("recieved invalid level");
                break;
        }

        Terminal.ClearScreen();

        Terminal.WriteLine(password.Anagram());
        Terminal.WriteLine("Please enter your password: ");

    }

    void CheckPassword(string input)
    {
        if(input == password)
        {
            Terminal.WriteLine("Well done");
        }
        else
        {
            Terminal.WriteLine("Incorrect Password");
        }
    }
}
