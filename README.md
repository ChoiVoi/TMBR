# TrackMoneyBeforeRegret (TMBR)

TrackMoneyBeforeRegret (TMBR) is a simple web application designed to help you track your income and expenditures. With TMBR, you can easily add, edit, and delete transactions to manage your finances effectively. The application is built using Razor Pages in .NET and utilizes SQLite for data storage.

## Features

- **Track Income and Expenditure:** Add transactions to log your income and expenses.
- **Edit/Delete Transactions:** Easily update or remove transactions to keep your financial records accurate.
- **User Authentication:** Secure login and registration system to keep your data private and safe.
- **Responsive Design:** A user-friendly interface that works on both desktop and mobile devices.

## Technologies Used

- **ASP.NET Core Razor Pages:** For building the web application.
- **SQLite:** As the database to store transaction data.
- **Entity Framework Core:** For database operations and management.

## Installation

1. **Clone the Repository:**
    ```bash
    git clone https://github.com/yourusername/TrackMoneyBeforeRegret.git
    cd TrackMoneyBeforeRegret
    ```

2. **Install Dependencies:**
    Make sure you have .NET SDK installed. Then, restore the project dependencies.
    ```bash
    dotnet restore
    ```

3. **Update Database:**
    Apply migrations to set up the SQLite database.
    ```bash
    dotnet ef database update
    ```

4. **Run the Application:**
    Start the application using the .NET CLI.
    ```bash
    dotnet run
    ```

5. **Access the Application:**
    Open your web browser and navigate to `http://localhost:5006`.

## Usage

### Adding a Transaction

1. Log in to your account.
2. Navigate to the "Add Income" or "Add Expenditure" page.
3. Fill out the form with the transaction details.
4. Click "Save" to add the transaction.

### Editing a Transaction

1. Log in to your account.
2. Navigate to the "Income Details" or "Expenditure Details" page.
3. Click the "Edit" button next to the transaction you want to update.
4. Update the transaction details and click "Save."

### Deleting a Transaction

1. Log in to your account.
2. Navigate to the "Income Details" or "Expenditure Details" page.
3. Click the "Delete" button next to the transaction you want to remove.


## Contact

For any questions or suggestions, please contact Jaywoo Choi at [https://jaywoochoi.netlify.app/](https://jaywoochoi.netlify.app/).
