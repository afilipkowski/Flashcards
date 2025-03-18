# Flashcards

**Flashcards** is a console application written in C# that I made in order to grasp the basics of working with SQL Server. The user can create thematic decks, add flashcards to them, and practice added vocabulary using the *Study* functionality.

# Tech stack
- C#
- SQL Server 2022
- Dapper
- [Spectre.Console](https://github.com/spectreconsole/spectre.console)

# Features
- Adding, deleting and editing flashcard stacks (to group flashcards thematically; every flashcard has to be assigned to a stack)

  ![obraz](https://github.com/user-attachments/assets/9280ed23-289f-4eb0-bbba-3c609df90314)

- Adding, deleting and editing flashcards; every flashcard needs to have a term and a definition

  ![obraz](https://github.com/user-attachments/assets/858e03ad-fb7b-4838-bf91-9d152c58c6d0)
  
- Study session functionality - the user is asked to provide definition of every term from selected flashcard stack; the application stores results of study sessions

  ![obraz](https://github.com/user-attachments/assets/8c5a4460-d153-40d7-a796-f5eb93041ee8)
  ![obraz](https://github.com/user-attachments/assets/c8e25dc6-fbbb-4163-b215-334763c5f430)

- Data is stored in SQL Server LocalDB. The database and tables are created automatically.

# Running the app

To run this app make sure you have installed:
- .NET 8.0
- SQL Server 2022 (with LocalDB)

1. Clone the repository:
    ```
    git clone https://github.com/afilipkowski/Flashcards
    cd Flashcards/Flashcards
    ```
2. Make sure your LocalDB works correctly (MSSQLLocalDB)
   ```
   sqllocaldb info
   ```
4. Build the project:
    ```
    dotnet build
    ```
5. Run the application:
    ```
    dotnet run
    ```
The database created by the app can be deleted using SQL Server Management Studio.
