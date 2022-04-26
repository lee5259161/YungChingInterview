using Service.InterFace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class UtilService : IUtilService
    {
        public string encryptPWD(string uid, string PWD)
        {
            // 將密碼轉為 SHA256 雜湊運算(不可逆)
            string salt = uid.Substring(0, 1).ToLower(); //使用帳號前一碼當作密碼鹽
            SHA256 sha256 = SHA256.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(salt + PWD); //將密碼鹽及原密碼組合
            byte[] hash = sha256.ComputeHash(bytes);
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));  //轉換成十六進位制，每次都是兩位數
            }
            string checkPwd = result.ToString(); // 雜湊運算後密碼

            return checkPwd;
        }
    }
}
