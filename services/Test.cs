



#region practise/learning
using System.Threading;
using System.Threading.Tasks;

namespace services.Test {
    class Test {

        public Test() {

        }

        public async Task<string> GetMessage() {
            string gach = await GashiBass();
            return gach;
        } 

        public async Task<string> GashiBass() {
            await Task.Delay(3000);
            return asyncGenerateMsg();;
         }      

        public string asyncGenerateMsg() {
            return "Salut à tous";
        }

    }
}
#endregion