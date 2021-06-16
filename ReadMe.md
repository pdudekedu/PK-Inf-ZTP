# Instalacja
Do uruchomienia aplikacji konieczne są:
* platforma .NET 5 - https://dotnet.microsoft.com/download/dotnet/5.0
* Visual Studio 2019 Community w wersji obsługującej .NET 5 (16.9+) - https://visualstudio.microsoft.com/pl/downloads/
* Visual Studio Code - https://code.visualstudio.com/download
* SQL Server 2019 Express - https://www.microsoft.com/en-us/download/details.aspx?id=101064

# Uruchomienie
Aby uruchomić projekt należy:
* w katalotu /WorkManager/ClientApp uruchomić comendę npm install
* uruchomić solucję (plik WorkManager.sln)
* odtworzyć paczki Nuget - prawym na solucję > Restore NuGet Packages
* przebudować solucję - zmenu górnego Build > Rebuild Solution
* utworzyć bazę danych - prawym na projekt WorkManager.Database > Publish... > wybrać lokalny serwer SQL Server -> Publish
* uzupełnic sekrety - prawym na projekt WorkManager > Manage User Secrets > wkleić i uzupełnić z właściwym ciągiem poąłczenia:
```
{
  "ConnectionStrings": {
    "WorkManager": "<ciąg połączenia do utworzonej bazy danych>"
  }
}
```
* uruchomić projekt (Ctrl + F5)