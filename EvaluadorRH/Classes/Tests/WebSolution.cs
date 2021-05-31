using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluadorRH.Classes.Tests
{
    public class WebSolution : Solution
    {
        private byte[] _Solution;
        public byte[] Solution
        {
            get => _Solution;
            set
            {
                _Solution = value as byte[];
                Raise(() => Solution);
            }
        }
        public async Task CaptureSolution()
        {
            Kit.WPF.Services.ScreenShotService service = new Kit.WPF.Services.ScreenShotService();
            this.Solution = await service.Capture();
        }

        internal override async Task Save(int MainTestId)
        {
            await CaptureSolution();
            await base.Save(MainTestId);
        }

        public override Solution Get(int value)
        {
            return AppData.SQLiteConnection.Find<WebSolution>(value);
        }
    }
}
