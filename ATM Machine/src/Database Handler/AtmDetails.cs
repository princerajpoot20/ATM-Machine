using ATM_Machine.src.Models;
using System.Security.Principal;
using System.Text;

namespace ATM_Machine.src.data;

public class AtmDetails
{
    private const string _atmDetailsPath =
        @"C:\Users\prajpoot\OneDrive - WatchGuard Technologies Inc\Project\ATM\ATM Machine\ATM Machine\src\Database\AtmDetails.csv";

    public static ATM getAtmDetails(int atmId)
    {
        AtmState atmState = AtmState.OutOfService;
        var location = "";
        var details = File.ReadAllLines(_atmDetailsPath);
        foreach (var detail in details)
        {
            var data = detail.Split(',');
            if (Convert.ToInt32(data[0])==atmId)
            {
                if (data[1]=="InService")atmState = AtmState.InService;
                else atmState = AtmState.OutOfService;
                location = data[2];
                break;
            }
        }
        return new ATM(atmId, atmState, location);
    }

    public static void updateAtmDetails(ATM atm)
    {
        StringBuilder builder = new StringBuilder();

        string line;
        using (StreamReader reader = new StreamReader(_atmDetailsPath))
        {
            while ((line = reader.ReadLine()) != null)
            {
                var data = line.Split(',');
                if (Convert.ToInt32(data[0]) == atm.AtmId)
                {
                    line = atm.AtmId + "," + atm.AtmState + "," + atm.AtmLocation;
                }
                builder.AppendLine(line);
            }
        }
        using (StreamWriter writer = new StreamWriter(_atmDetailsPath, false)) // false to overwrite the file
        {
            writer.Write(builder.ToString());
        }
    }
}