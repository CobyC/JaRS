using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JARS.Test.Miscellaneous
{
    [TestClass]
    public class PasswordStringTest
    {


        [TestMethod]
        public void Test_if_string_as_password_is_strong()
        {
            string p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14;
            p1 = "weak"; //fail
            p2 = "weakisha"; //fail
            p3 = "W3akishb"; //fail
            p4 = "W3a!-."; //fail
            p5 = " Tab 12/"; //fail
            p6 = "Sh0l6B3_$Tr0nG"; //pass
            p7 = @"¬!""£$%^&*()_+\/,.;:'@#~[{]}-+=<>|?";//fail
            p8 = @"QF&*6@h\";//pass
            p9 = @"z\NhUJ2";//pass
            p10 = @"sh7q^J:!N";//pass
            p11 = @"D.k$V}H7D""";//pass
            p12 = @"n^AsG1.B'tB";//pass
            p13 = @"z4P'Z&].]&aa";//pass
            p14 = @"pYu \#m$[:w[U";//fail has no number

            Assert.IsFalse(PasswordTester(p1));
            Assert.IsFalse(PasswordTester(p2));
            Assert.IsFalse(PasswordTester(p3));
            Assert.IsFalse(PasswordTester(p4));
            Assert.IsTrue(PasswordTester(p5));
            Assert.IsTrue(PasswordTester(p6));
            Assert.IsFalse(PasswordTester(p7));
            Assert.IsTrue(PasswordTester(p8));//fails on /
            Assert.IsTrue(PasswordTester(p9));
            Assert.IsTrue(PasswordTester(p10));
            Assert.IsTrue(PasswordTester(p11));
            Assert.IsTrue(PasswordTester(p12));
            Assert.IsTrue(PasswordTester(p13));
            Assert.IsFalse(PasswordTester(p14));


            Assert.IsFalse(PasswordTesterRegx(p1));
            Assert.IsFalse(PasswordTesterRegx(p2));
            Assert.IsFalse(PasswordTesterRegx(p3));
            Assert.IsFalse(PasswordTesterRegx(p4));
            Assert.IsTrue(PasswordTesterRegx(p5));
            Assert.IsTrue(PasswordTesterRegx(p6));
            Assert.IsFalse(PasswordTesterRegx(p7));
            Assert.IsTrue(PasswordTesterRegx(p8));
            Assert.IsTrue(PasswordTesterRegx(p9));
            Assert.IsTrue(PasswordTesterRegx(p10));
            Assert.IsTrue(PasswordTesterRegx(p11));
            Assert.IsTrue(PasswordTesterRegx(p12));
            Assert.IsTrue(PasswordTesterRegx(p13));
            Assert.IsFalse(PasswordTesterRegx(p14));


        }

        bool PasswordTester(string password)
        {
            bool hasUpp = false;
            bool hasLow = false;
            bool hasDig = false;
            bool hasChar = false;
            bool hasLenth = false;
            bool isValid = false;
            if (password.Length > 6)
                hasLenth = true;
            else
                return hasLenth;
            
            foreach (char ch in password)
            {
                if (!hasUpp && char.IsUpper(ch))
                    hasUpp = true;
                if (!hasLow && char.IsLower(ch))
                    hasLow = true;
                if (!hasDig && char.IsDigit(ch))
                    hasDig = true;
                if ((!hasChar && !char.IsControl(ch)) && (char.IsSymbol(ch) || char.IsPunctuation(ch)))
                    hasChar = true;
            }

            if (hasUpp && hasLow && hasDig && hasChar)
                isValid = true;

            return isValid;
        }
        bool PasswordTesterRegx(string password)
        {
            bool isValid = Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{7,15}$");
            return isValid;
        }

    }
}
