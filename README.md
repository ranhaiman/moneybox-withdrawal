# Moneybox Money Withdrawal

The solution contains a .NET core library (Moneybox.App) which is structured into the following 3 folders:

* Domain - this contains the domain models for a user and an account, and a notification service.
* Features - this contains two operations, one which is implemented (transfer money) and another which isn't (withdraw money)
* DataAccess - this contains a repository for retrieving and saving an account (and the nested user it belongs to)

## The task

The task is to implement a money withdrawal in the WithdrawMoney.Execute(...) method in the features folder. For consistency, the logic should be the same as the TransferMoney.Execute(...) method i.e. notifications for low funds and exceptions where the operation is not possible. 

As part of this process however, you should look to refactor some of the code in the TransferMoney.Execute(...) method into the domain models, and make these models less susceptible to misuse. We're looking to make our domain models rich in behaviour and much more than just plain old objects, however we don't want any data persistance operations (i.e. data access repositories) to bleed into our domain. This should simplify the task of implementing WithdrawMoney.Execute(...).

## Guidelines

* You should spend no more than 1 hour on this task, although there is no time limit
* You should fork or copy this repository into your own public repository (Github, BitBucket etc.) before you do your work
* Your solution must compile and run first time
* You should not alter the notification service or the the account repository interfaces
* You may add unit/integration tests using a test framework (and/or mocking framework) of your choice
* You may edit this README.md if you want to give more details around your work (e.g. why you have done something a particular way, or anything else you would look to do but didn't have time)

Once you have completed your work, send us a link to your public repository.

Good luck!

I’ve done the following changes:
1. Decouple the Moneybox.App to three separate class libraries: Model, DataAccess and App. 
   This is for better maintainability, testability and in case of replacing to different components (like data access).
2. Refactoring the TransferMoney object by moving the account logic to the account object,
   And also moving the user related logic to the user object.
3. Implementing the WithdrawMoney object in the same way as TransferMoney object.
4. Interduce DependencyInjection (although not being used).
5. Add unit testing which helped with validating the exciting logic, validating the logic through refactoring and while the adding new logic.


Things I would do next:
1. Adding comments to the interfaces describing the business of the components.
2. Creating business exceptions instead of using system exceptions.