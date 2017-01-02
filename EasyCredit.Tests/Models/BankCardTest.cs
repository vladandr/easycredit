using Microsoft.VisualStudio.TestTools.UnitTesting;
using EasyCredit.Models;

namespace EasyCredit.Tests.Models
{
    [TestClass]
    public class BankCardTest
    {
        [TestMethod]
        public void TestBankCardCanStoreMoney()
        {
            //BankAccount bankAccount = new BankAccount();
            BankCard bankCard = new BankCard();
            //bankCard.Account = bankAccount;
            bankCard.Money = 150;
            //bankAccount.BankCards.Add(bankCard);
            Assert.AreEqual(bankCard.Money, 150);
        }
    }
}
