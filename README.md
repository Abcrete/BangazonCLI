# Bangazon

## The Best Command Line Ordering System

.NET CLI that allows users to create customers, products, orders, payments, and use that data to complete orders and generate reports.

## Ordering System Interface

### Main Menu

```bash
*********************************************************
**  Welcome to Bangazon! Command Line Ordering System  **
*********************************************************
1. Create a customer account
2. Choose active customer
3. Create a payment option
4. Add product to sell
5. Add product to shopping cart
6. Complete an order
7. Remove customer product
8. Update product information
9. Show stale products
10. Show customer revenue report
11. Show popular products
12. Leave Bangazon
>
```

### How to install and run

1. Clone repository from https://github.com/Abcrete/BangazonCLI.git
3. Create and environment variable name "BANGAZON_TEST_DB" pointing to the location you would like your bangazon database.
2. Navigate your terminal to src/Bangazon/ folder within
3. Enter 'dotnet restore'
4. Enter 'dotnet build'
5. Enter 'dotnet run'

### Data tables and Classes

1. Customer - Get all customers, add customer
2. Product - Get all products, modify existing product, delete product
4. Product Type - Get all product types, create new product type
3. Payment Type - Get all payment types for a customer, add a new payment type
5. Order - Get all orders for a customer, create order, add product to order, pay for order, get total order cost
6. PopularProduct - Get a list of popular products based on criteria given

## This application consists of:

1. System.ValueTuple
2. Microsoft SQLite
