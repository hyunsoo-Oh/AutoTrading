using System.Security.Cryptography;
using System.Text;

namespace KisRestAPI.Common.WebSocket
{
    /// <summary>
    /// 한국투자증권 WebSocket 실시간 데이터 AES-256-CBC 복호화
    ///
    /// 체결통보 등 암호화된 구독 데이터를 복호화할 때 사용한다.
    /// IV와 Key는 구독 응답 JSON(output.iv / output.key)에서 제공된다.
    /// </summary>
    internal static class KisWebSocketDecryptor
    {
        /// <summary>
        /// AES-256-CBC 복호화
        /// </summary>
        /// <param name="cipherText">Base64 인코딩된 암호문</param>
        /// <param name="key">구독 응답에서 받은 AES Key (UTF-8 문자열)</param>
        /// <param name="iv">구독 응답에서 받은 AES IV (UTF-8 문자열)</param>
        /// <returns>복호화된 평문</returns>
        public static string Decrypt(string cipherText, string key, string iv)
        {
            if (string.IsNullOrEmpty(cipherText))
                throw new ArgumentException("복호화 대상이 비어 있습니다.", nameof(cipherText));
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("AES Key가 비어 있습니다.", nameof(key));
            if (string.IsNullOrEmpty(iv))
                throw new ArgumentException("AES IV가 비어 있습니다.", nameof(iv));

            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] ivBytes = Encoding.UTF8.GetBytes(iv);
            byte[] cipherBytes = Convert.FromBase64String(cipherText);

            using var aes = Aes.Create();
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = keyBytes;
            aes.IV = ivBytes;

            using var decryptor = aes.CreateDecryptor();
            byte[] plainBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);

            return Encoding.UTF8.GetString(plainBytes);
        }
    }
}
