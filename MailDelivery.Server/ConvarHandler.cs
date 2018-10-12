using System;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace MailDelivery.Server
{
    internal class ConVarHandler : BaseScript
    {
        private static bool _debug = false;

        private static int _minPayment = 0;
        private static int _maxPayment = 0;
        private static int _rentalAmount = 0;
        private static int _jobCooldown = 0;

        public ConVarHandler()
        {
            EventHandlers.Add("onServerResourceStart", new Action<string>(OnServerResourceStart));
            EventHandlers.Add("MailDelivery:GetConvars", new Action<Player>(OnGetConvars));
        }

        private void OnServerResourceStart(string resourceName)
        {
            if (GetCurrentResourceName() != resourceName)
            {
                return;
            }

            _minPayment = GetConvarInt("mail_min_payment", 150);
            _maxPayment = GetConvarInt("mail_max_payment", 1000);
            _rentalAmount = GetConvarInt("mail_rental_amount", 2000);
            _jobCooldown = GetConvarInt("mail_job_cooldown", 60000);
            _debug = Convert.ToBoolean(GetConvar("mail_debug", "false"));
        }

        private void OnGetConvars([FromSource]Player player)
        {
            if (_debug)
            {
                Debug.WriteLine("Player retrieved Convars");
            }

            player.TriggerEvent("MailDelivery:Initialize", _minPayment, _maxPayment, _rentalAmount, _jobCooldown, _debug);
        }
    }
}