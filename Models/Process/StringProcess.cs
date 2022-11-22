using System.Text.RegularExpressions;
namespace BuiThiKimNganBTH2.Models.Process
{
    public class StringProcess

{

    public string AutoGenerateCode( string strInput)

    {

        // viết code xử lý sinh mã tự động

        string strResult = "", numPart = "", strPart ="";
       // tach phan so tu strInput
       // VD: strInput = "STD001" => numPart ="001"
       numPart = Regex.Match(strInput,@"\d+").Value;
       // tach phan chu tu strInput
       //VD: strInput ="STD001" => strPart ="STD"
       strPart = Regex.Match(strInput,@"\D+").Value;
       // tang phan so len 1 don vi
       int intPart = (Convert.ToInt32(numPart)+1);
       // bo sung cac ky tu 0 còn thieu
       for (int i= 0; i<numPart.Length - intPart.ToString().Length;i++)
       {
          strPart +="0";
       }
        strResult = strPart + intPart;
        return strResult;
    
     
    }
    

       
        //B1: Tách phần số và chữ của key
        //B2: Chuyển phần số sang kiểu int và tăng lên một đơn vị

        //B3: Ghép phần chữ và phần số mới( sau lên tăng 1 đơn vị )

        // ví dụ: key =STD019 => Mã sinh ra là: STD020

        // b4: Gán giá trị ở bước 3 cho newKey và trả về

        

    }

}
