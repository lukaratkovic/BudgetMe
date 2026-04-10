# BudgetMe
Take control of your finances with BudgetMe, a simple web app that allows you to import your bank activity reports, categorize transactions, and overview your spending habits.

## My Transactions
The My transactions page is the main view of the application. It contains a list of all transactions imported or manually added into the application, with sorting and filtering options by transaction type (income, expense), categories, transaction descriptions, dates of transactions, and the amounts saved or earned.

From the transactions page, you can add new transaction, edit and delete existing ones, or import transactions based on your bank activity report. The import functionality currently only supports Zagrebačka banka format, but support for custom formats is one of the planned future features.

## Settings
The settings menu allows you to add new categories and create custom bindings to simplify the categorization of transactions imported from bank activity reports.

### Transaction Categories
The BudgetMe application comes with a number of pre-existing categories for both income and expenses. If you find yourself missing a category, you can easily add it from the Transaction Categories page. While pre-existing categories cannot be edited nor deleted, you are free to create, edit, and delete user-defined categories to your liking.

### Bindings
By default, when you import transactions from a bank activity report, you will have to open each transaction and manually add its categories. If you are importing a large amount of transactions, this can become tedious very quickly. That is why, with Bindings, you can create custom rules that will determine which imported transaction is marked with which category.

The way Bindings work is simple. First, you define a keyword, transaction type, and category. Then, when you begin the import of your bank activity report, the application will automatically compare each transaction's description with existing bindings, checking whether it contains the defined keyword. If a binding with the correct keyword and transaction type is found, the transaction is automatically marked with the corresponding category.

## Upcoming features
This application is still in development, and there are some features which I intend on adding that I have not gotten around to yet:

* Category and Bindings filtering
* Custom import settings, allowing the user to define the start row of their bank activity report, as well as the columns containing the data necessary for import
* Reports, giving a user a better overview of their spending habits
