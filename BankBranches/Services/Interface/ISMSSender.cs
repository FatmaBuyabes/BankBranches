namespace BankBranches.Services.Interface
{
    public interface ISMSSender
    {
        int checkBalance();
        void SendSms(string phoneNumber, string message);
    }

    public class ZainSMSSender : ISMSSender
    {
        public int checkBalance()
        {
            return 1000;
            throw new NotImplementedException();
        }

        public bool SendSms(string phoneNumber, string message)
        {
            return true;
        }

        void ISMSSender.SendSms(string phoneNumber, string message)
        {
            throw new NotImplementedException();
        }
    }
}
