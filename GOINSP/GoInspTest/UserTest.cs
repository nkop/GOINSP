using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GOINSP.ViewModel;
using GOINSP.Models;

namespace GoInspTest
{
    [TestClass]
    public class UserTest
    {
        GOINSP.Models.Context context = new GOINSP.Models.Context();
        Account account;
        AccountManagementVM amvm;
        AccountVM accVM;

        [TestInitialize]
        public void Init()
        {
            amvm = new AccountManagementVM();
            account = new Account();
            account.Password = "123";
            account.UserName = "admin";
            account.Email = "account@goinsp.nl";

            accVM = new AccountVM();
            accVM.UserName = "admin";
            accVM.Password = "123";
            accVM.Email = "account@goinsp.nl";
        }

        [TestMethod]
        public void CreateNewUserTest()
        {
            //Arrange
            
            //Act
           
           //Assert
           
                    
        }
        [TestMethod]
        public void CreateNewUserTestT()
        {
            //Arrange
            Account acc = new Account();

            acc.Password = "123";
            acc.UserName = "admin";
            acc.Email = "account@goinsp.nl";



            //Act
            amvm.CreateAccountCommand.Execute(acc);
            //Assert
            Assert.AreEqual(account.UserName, acc.UserName);
            Assert.IsTrue(amvm.CreateAccountCommand.CanExecute(acc));

        }
        [TestMethod]
        public void LoginTest()
        {
            //
            amvm.LoginName = account.UserName;
            amvm.LoginCommand.Execute("admin");
            Assert.IsTrue(amvm.LoginCommand.CanExecute("admin"));
        }
        [TestMethod]
        public void DeleteUserTest()
        {
            amvm.SelectedAccount = accVM;
            amvm.DeleteUserCommand.Execute("admin");
            Assert.IsTrue(amvm.DeleteUserCommand.CanExecute("admin"));
        }
      //  [TestMethod]
      //  public void ShowAddUserTest()
//{
       //     amvm.ShowAddUserCommand.Execute(null);
      //      Assert.IsTrue(amvm.ShowAddUserCommand.CanExecute(null));
       // }
      //  [TestMethod]
       // public void ForgottenPasswordTest()
      //  {
       //     amvm.VergetenCommand.Execute(null);
       //     Assert.IsTrue(amvm.VergetenCommand.CanExecute(null));
       // }
    }
}
