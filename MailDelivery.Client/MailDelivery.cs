using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.UI;
using static CitizenFX.Core.Native.API;

namespace MaillDelivery.Client
{
    internal class MailDelivery : BaseScript
    {
        private static readonly List<Vector4> VehicleSpawnPositions = new List<Vector4>
        {
            new Vector4(-400.5178f, 6163.97607f, 31.387895f, 354.08728f),
            new Vector4(-403.4285f, 6165.96875f, 31.426778f, 352.25277f),
            new Vector4(-406.0371f, 6168.61914f, 31.394813f, 353.80203f),
            new Vector4(-408.9385f, 6171.53271f, 31.380252f, 353.44647f),
            new Vector4(-411.9129f, 6174.58349f, 31.379669f, 353.77667f)
        };

        private static readonly Dictionary<string, List<Vector3>> DeliveryAreas = new Dictionary<string, List<Vector3>>
        {
            { "Grapeseed", new List<Vector3> {
                    new Vector3(2563.99f, 4692.7f, 35.02f),
                    new Vector3(1967.33f, 4640.92f, 41.88f),
                    new Vector3(2030.39f, 4980.46f, 42.1f),
                    new Vector3(1717.86f, 4676.93f, 43.66f),
                    new Vector3(1689.25f, 4818.3f, 43.06f),
                    new Vector3(2505.48f, 4095.73f, 39.2f), // 5
                    new Vector3(2570.87f, 4282.84f, 43.0f),
                    new Vector3(2721.19f, 4285.98f, 48.6f),
                    new Vector3(2727.59f, 4145.46f, 45.69f),
                    new Vector3(3322.6f, 5166.06f, 19.92f),
                    new Vector3(2216.42f, 5612.49f, 55.69f), // 10
                    new Vector3(2434.51f, 4976.82f, 47.07f),
                    new Vector3(2300.36f, 4871.94f, 42.06f),
                    new Vector3(1962.36f, 5184.98f, 47.98f),
                    new Vector3(1698.97f, 4921.18f, 42.56f),
                    new Vector3(1655.87f, 4874.38f, 42.04f), // 15
                    new Vector3(2159.72f, 4789.8f, 41.67f),
                    new Vector3(2121.77f, 4784.71f, 41.97f),
                    new Vector3(2639.04f, 4246.56f, 44.77f),
                    new Vector3(2455.85f, 4058.3f, 38.06f),
                    new Vector3(3680.06f, 4497.93f, 25.11f), // 20
                    new Vector3(3807.8f, 4478.6f, 6.37f)
                }
            },
            { "Sandy Shores", new List<Vector3> {
                    new Vector3(1986.69824f, 3815.02490f, 33.32370f),
                    new Vector3(1446.34997f, 3649.69384f, 35.48260f),
                    new Vector3(228.27f, 3165.8f, 43.61f),
                    new Vector3(170.36f, 3113.28f, 43.51f),
                    new Vector3(179.76f, 3033.1f, 43.98f),
                    new Vector3(1990.57141f, 3057.46801f, 48.06378f), // 5
                    new Vector3(2201.01f, 3318.25f, 46.77f),
                    new Vector3(2368.38f, 3155.96f, 48.61f),
                    new Vector3(1881.07f,3888.5f,34.25f),
                    new Vector3(1889.76f,3810.71f,33.75f),
                    new Vector3(1638.8f,3734.17f,34.41f), // 10
                    new Vector3(2630.27f,3262.88f,56.25f),
                    new Vector3(2622.43f,3275.56f,56.3f),
                    new Vector3(2633.7f,3287.4f,56.45f),
                    new Vector3(2389.48f, 3341.64f, 47.72f), // 15
                    new Vector3(2393.01f, 3320.62f, 48.24f),
                    new Vector3(2163.38f, 3374.63f, 46.07f),
                    new Vector3(1959.95f, 3741.99f, 33.24f),
                    new Vector3(1931.55f, 3727.6f, 33.84f),
                    new Vector3(1850.68f, 3690.03f, 35.5f), // 20
                    new Vector3(1707.92f, 3585.29f, 36.57f),
                    new Vector3(1756.33f, 3659.54f, 35.39f),
                    new Vector3(1825.41f, 3718.35f, 34.42f),
                    new Vector3(1899.13f, 3764.68f, 33.79f),
                    new Vector3(1923.37f, 3797.43f, 33.44f), // 25
                    new Vector3(1914.69f, 3813.37f, 33.44f),
                    new Vector3(1913.61f, 3868.06f, 33.37f),
                    new Vector3(1942.34f, 3885.42f, 33.67f),
                    new Vector3(1728.66f, 3851.46f, 34.78f),
                    new Vector3(903.67f, 3560.82f, 33.81f),
                    new Vector3(910.93f, 3644.29f, 32.68f),
                    new Vector3(1393.15f,3673.4f, 34.79f),
                    new Vector3(1435.18f, 3682.92f, 34.84f),
                }
            },
            { "Paleto Bay", new List<Vector3>
                {
                    new Vector3(-291.14f, 6199.27f, 32.49f), // 0
                    new Vector3(-96.43f, 6324.47f, 32.08f),
                    new Vector3(-390.28f, 6300.23f, 30.75f),
                    new Vector3(-360.8f, 6320.98f, 30.76f),
                    new Vector3(-303.41f, 6329f, 32.99f),
                    new Vector3(-215.5f, 6431.99f, 32.49f), // 5
                    new Vector3(-46.21f,6595.62f,31.55f),
                    new Vector3(0.46f, 6546.92f, 32.37f),
                    new Vector3(-1.09f, 6512.9f, 33.04f),
                    new Vector3(99.35f, 6618.56f, 33.47f),
                    new Vector3(-774.31f, 5597.84f, 34.61f), // 10
                    new Vector3(-696.1f, 5802.36f, 17.83f),
                    new Vector3(-448.77f, 6009.95f, 32.22f),
                    new Vector3(-326.55f,6083.95f,31.96f),
                    new Vector3(-341.66f, 6212.46f,32.59f),
                    new Vector3(-247.15f,6331.02f,32.93f), // 15
                    new Vector3(-394.74f,6272.52f,30.94f),
                    new Vector3(35.18f,6662.39f,32.19f),
                    new Vector3(-130.66f,6551.98f,29.87f),
                    new Vector3(-106.06f,6469.6f,32.63f),
                    new Vector3(-94.5f, 6408.86f, 32.14f), // 20
                    new Vector3(-25.2f,6472.25f,31.98f),
                    new Vector3(-105.28f,6528.96f,30.17f),
                    new Vector3(150.41f,6647.58f,32.11f),
                    new Vector3(161.68f,6636.1f,32.17f),
                    new Vector3(-9.37f,6653.93f,31.98f), //25
                    new Vector3(-40.15f,6637.23f,31.09f),
                    new Vector3(-5.97f,6623.07f,32.32f),
                    new Vector3(-113.22f, 6538.18f, 30.6f),
                }
            },
        };

        private static readonly List<Vector3> DeliveryAreaWide = new List<Vector3>
        {
            new Vector3(2643.8f, 4784.91f, 33.52f),
            new Vector3(1540.14f, 3137.63f, 40.43f),
            new Vector3(-69.5f, 5955.26f, 128.92f)
        };

        private static readonly Vector3 DutyPosition = new Vector3(-427.28f, 6130.83f, 28.48f);
        private static readonly Vector3 RentalPosition = new Vector3(-411.67f, 6181.37f, 28.48f);

        private static readonly Color DutyColor = Color.FromArgb(255, 92, 133, 255);

        private static bool _isFirstTick = true;
        private static bool _debug = false;
        private bool _isOnDuty;

        private static Random rnd = new Random();
        private Vehicle _jobVehicle;

        private string _currentArea = "";
        private List<Vector3> _currentAreaDeliveryPositions = new List<Vector3>();
        private List<Blip> _currentAreaDeliveryBlips = new List<Blip>();

        private static int _minPayment = 0;
        private static int _maxPayment = 0;
        private static int _rentalAmount = 0;
        private static int _jobCooldown = 0;
        private static int _areaIndex = 0;
        private static int _testTotal = 0;
        private static string _vanModel = "";

        public MailDelivery()
        {
            Initialize();
            CreateBlip(DutyPosition, (BlipSprite)67, BlipColor.White, "Mail Delivery");
            CreateBlip(RentalPosition, (BlipSprite)67, BlipColor.White, "Mail Delivery : Rental");

            if (_debug)
            {
                foreach (var pos in DeliveryAreaWide)
                {
                    CreateBlip(pos, BlipSprite.Standard, BlipColor.Red, "Delivery Area Center");
                }

                foreach (var pos in DeliveryAreas["Grapeseed"])
                {
                    CreateBlip(pos, BlipSprite.PointOfInterest, BlipColor.Red, "POI GrapeSeed");
                }

                foreach (var pos in DeliveryAreas["Paleto Bay"])
                {
                    CreateBlip(pos, BlipSprite.PointOfInterest, BlipColor.FranklinGreen, "POI Paleto Bay");
                }

                foreach (var pos in DeliveryAreas["Sandy Shores"])
                {
                    CreateBlip(pos, BlipSprite.PointOfInterest, BlipColor.TrevorOrange, "POI Sandy Shores");
                }

                Tick += OnTestTick;
            }

            Tick += OnDutyTick;
            Tick += OnDeliveryTick;
            Tick += OnRentalTick;
            Tick += OnMarkerTick;
        }

        private async Task OnDutyTick()
        {
            try
            {
                if (_isFirstTick)
                {
                    _jobVehicle = new Vehicle(0);
                    _isFirstTick = false;
                    return;
                }

                var playerPos = Game.PlayerPed.Position;

                if (playerPos.DistanceToSquared2D(DutyPosition) < 16)
                {
                    Screen.DisplayHelpTextThisFrame($"Press ~INPUT_CONTEXT~ to {(_isOnDuty ? "~r~Stop Delivering Mail" : "~g~Deliver Mail")}~s~.");

                    if (Game.IsControlJustReleased(0, Control.Context))
                    {
                        var playerPed = Game.PlayerPed;

                        if (!playerPed.IsSittingInVehicle() || playerPed.CurrentVehicle.Model.Hash != new Model(_vanModel).Hash)
                        {
                            Screen.DisplayHelpTextThisFrame("You are not driving a ~r~PostOP Boxville~s~! You can rent one at the ~g~Mail Delivery: Rental~s~.");

                            await Delay(5000);

                            return;
                        }

                        ToggleDuty(!_isOnDuty);

                        if (!_isOnDuty)
                        {
                            World.RemoveWaypoint();

                            foreach (var blip in _currentAreaDeliveryBlips)
                            {
                                blip.Delete();
                            }

                            _currentAreaDeliveryBlips.Clear();

                            return;
                        }

                        var route = DeliveryAreas.ElementAt(rnd.Next(0, DeliveryAreas.Count));

                        _currentArea = route.Key;
                        _currentAreaDeliveryPositions = route.Value.OrderBy(x => rnd.Next()).Take(rnd.Next(10, 12)).ToList();

                        foreach (var pos in _currentAreaDeliveryPositions)
                        {
                            var b = CreateBlip(pos, BlipSprite.PointOfInterest, BlipColor.Yellow, "Delivery Point", false, 1f);
                            _currentAreaDeliveryBlips.Add(b);
                        }

                        _areaIndex = 0;

                        switch (_currentArea)
                        {
                            case "Grapeseed":
                                _areaIndex = 0;
                                break;
                            case "Sandy Shores":
                                _areaIndex = 1;
                                break;
                            case "Paleto Bay":
                                _areaIndex = 2;
                                break;
                        }

                        Screen.ShowSubtitle($"Deliver ~g~{_currentAreaDeliveryPositions.Count}x~s~ mail and packages in ~y~{_currentArea}", 5000);

                        _isOnDuty = true;

                        await Delay(_jobCooldown);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"{e.Message} : Exception thrown on MailDelivery:OnDutyTick()");
            }

            await Task.FromResult(0);
        }

        private async Task OnDeliveryTick()
        {
            try
            {
                if (!_isOnDuty)
                {
                    return;
                }

                var playerPed = Game.PlayerPed;

                if (playerPed.IsGettingIntoAVehicle || playerPed.LastVehicle == null)
                {
                    return;
                }

                if (playerPed.LastVehicle.Model.Hash != new Model(_vanModel).Hash)
                {
                    return;
                }


                var playerPos = Game.PlayerPed.Position;

                if (playerPos.DistanceToSquared2D(DeliveryAreaWide[_areaIndex]) > 2000000)
                {
                    await Delay(4000);
                    return;
                }

                foreach (var pos in _currentAreaDeliveryPositions)
                {
                    if (playerPos.DistanceToSquared2D(pos) < 100)
                    {
                        World.DrawMarker(MarkerType.ChevronUpx1, pos, Vector3.Zero, new Vector3(0, 180f, 0), new Vector3(0.5f), DutyColor, false, true, false);
                    }

                    if (playerPos.DistanceToSquared2D(pos) < 4)
                    {
                        if (playerPed.IsSittingInVehicle())
                        {
                            Screen.DisplayHelpTextThisFrame("~r~Exit~s~ your Mail Delivery Van.");
                            return;
                        }

                        Screen.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to Deliver Mail.");

                        if (Game.IsControlJustReleased(0, Control.Context))
                        {
                            var _payment = rnd.Next(_minPayment, _maxPayment);

                            if (_debug)
                            {
                                _testTotal += _payment;
                            }

                            playerPed.Task.PlayAnimation("mp_safehouselost@", "package_dropoff");
                            await Delay(1000);

                            TriggerServerEvent("MailDelivery:DeliveryMade", _payment);

                            _currentAreaDeliveryBlips.Single(x => x.Position == pos).Delete();
                            _currentAreaDeliveryPositions.Remove(pos);

                            if (_currentAreaDeliveryPositions.Count != 0)
                            {
                                Screen.ShowNotification($"Delivered Mail for ~g~${_payment}~s~.");

                                if (_debug)
                                {
                                    Screen.ShowSubtitle($"~r~DEBUG~s~ Current Total: ~g~{_testTotal}~s~.");
                                }

                                return;
                            }

                            Screen.ShowNotification($"Delivered last Mail for ~g~${_payment}~s~.");
                            Screen.ShowNotification("~y~Return to PostOp for another Route~s~.");

                            if (_debug)
                            {
                                Screen.ShowSubtitle($"~r~DEBUG~s~ End Total Earned: ~g~{_testTotal}~s~.");
                            }

                            ToggleDuty(!_isOnDuty);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"{e.Message} : Exception thrown on MailDelivery:OnDeliveryTick()");
            }

            await Task.FromResult(0);
        }

        private async Task OnRentalTick()
        {
            try
            {
                var playerPos = Game.PlayerPed.Position;

                if (playerPos.DistanceToSquared2D(RentalPosition) < 16)
                {
                    var playerPed = Game.PlayerPed;
                    if (playerPed.IsSittingInVehicle())
                    {
                        if (!DecorExistOn(playerPed.CurrentVehicle.Handle, "MailDelivery.Rental"))
                        {
                            Screen.DisplayHelpTextThisFrame("Exit your vehicle to rent a Mail Delivery Van");
                            return;
                        }

                        Screen.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to return Mail Delivery Van");
                        if (Game.IsControlJustReleased(0, Control.Context))
                        {
                            playerPed.CurrentVehicle.Delete();
                            Screen.ShowNotification("Mail Delivery Van has been returned.");
                        }

                        return;
                    }

                    Screen.DisplayHelpTextThisFrame($"Press ~INPUT_CONTEXT~ to rent Mail Delivery Van for ${_rentalAmount}");

                    if (Game.IsControlJustReleased(0, Control.Context))
                    {
                        var _parkId = GetParkingPosition(VehicleSpawnPositions);

                        if (_parkId < 0 || _parkId >= VehicleSpawnPositions.Count)
                        {
                            Screen.ShowNotification("There are no parking spots available.");
                            return;
                        }

                        var parkPos = VehicleSpawnPositions[_parkId];

                        _jobVehicle = await World.CreateVehicle(new Model(_vanModel), new Vector3(parkPos.X, parkPos.Y, parkPos.Z), parkPos.W);

                        SetEntityAsMissionEntity(_jobVehicle.Handle, true, true);

                        if (!DecorExistOn(_jobVehicle.Handle, "MailDelivery.Rental"))
                        {
                            DecorRegister("MailDelivery.Rental", 2);
                            DecorSetBool(_jobVehicle.Handle, "MailDelivery.Rental", true);
                        }

                        TriggerServerEvent("MailDelivery:VanRented", _rentalAmount);

                        Screen.DisplayHelpTextThisFrame("Your Mail Delivery Van is ready on one of the parking spots.");

                        await Delay(4000);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"{e.Message} : Exception thrown on MailDelivery:OnRentalTick()");
            }

            await Task.FromResult(0);
        }

        private async Task OnMarkerTick()
        {
            try
            {
                var playerPos = Game.PlayerPed.Position;

                if (playerPos.DistanceToSquared2D(RentalPosition) < 2500)
                {
                    World.DrawMarker(MarkerType.VerticalCylinder, RentalPosition, Vector3.Zero, Vector3.Zero, new Vector3(3.0f), DutyColor);
                }

                if (playerPos.DistanceToSquared2D(DutyPosition) < 2500)
                {
                    World.DrawMarker(MarkerType.VerticalCylinder, DutyPosition, Vector3.Zero, Vector3.Zero, new Vector3(3.0f), DutyColor);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"{e.Message} : Exception thrown on MailDelivery:OnMarkerTick()");
            }

            await Task.FromResult(0);
        }

        private async Task OnTestTick()
        {
            try
            {
                var playerPos = Game.PlayerPed.Position;
                if (playerPos.DistanceToSquared(DeliveryAreaWide[0]) < 1900000)
                {
                    foreach (var pos in DeliveryAreas["Grapeseed"])
                    {
                        if (playerPos.DistanceToSquared(pos) < 100)
                        {
                            World.DrawMarker(MarkerType.ChevronUpx1, pos, Vector3.Zero, new Vector3(0, 180f, 0), new Vector3(0.5f), DutyColor, true, true, true);
                            Screen.ShowSubtitle($"Index: {DeliveryAreas["Grapeseed"].IndexOf(pos)}");
                        }
                    }
                }

                if (playerPos.DistanceToSquared(DeliveryAreaWide[1]) < 1900000)
                {
                    foreach (var pos in DeliveryAreas["Sandy Shores"])
                    {
                        if (playerPos.DistanceToSquared(pos) < 100)
                        {
                            World.DrawMarker(MarkerType.ChevronUpx1, pos, Vector3.Zero, new Vector3(0, 180f, 0), new Vector3(0.5f), DutyColor, true, true, true);
                            Screen.ShowSubtitle($"Index: {DeliveryAreas["Sandy Shores"].IndexOf(pos)}");
                        }
                    }
                }

                if (playerPos.DistanceToSquared(DeliveryAreaWide[2]) < 1900000)
                {
                    foreach (var pos in DeliveryAreas["Paleto Bay"])
                    {
                        if (playerPos.DistanceToSquared(pos) < 100)
                        {
                            World.DrawMarker(MarkerType.ChevronUpx1, pos, Vector3.Zero, new Vector3(0, 180f, 0), new Vector3(0.5f), DutyColor, true, true, true);
                            Screen.ShowSubtitle($"Index: {DeliveryAreas["Paleto Bay"].IndexOf(pos)}");

                        }
                    }
                }

            }
            catch (Exception e)
            {
                Debug.WriteLine($"{e.Message} : Exception thrown on MailDelivery:OnTestTick()");
            }

            await Task.FromResult(0);
        }

        private void ToggleDuty(bool state)
        {
            _isOnDuty = state;
        }

        private int GetParkingPosition(List<Vector4> ParkingSpots)
        {
            for (int i = 0; i < ParkingSpots.Count; i++)
            {
                if (GetClosestVehicle(ParkingSpots[i].X, ParkingSpots[i].Y, ParkingSpots[i].Z, 3, 0, 70) == 0)
                {
                    return i;
                }
            }
            return -1;
        }

        private void Initialize()
        {
            _minPayment = GetConvarInt("mail_min_payment", 150);
            _maxPayment = GetConvarInt("mail_max_payment", 1000);
            _rentalAmount = GetConvarInt("mail_rental_amount", 2000);
            _jobCooldown = GetConvarInt("mail_job_cooldown", 60000);
            _vanModel = GetConvar("mail_van_model", "BOXVILLE4");
            _debug = Convert.ToBoolean(GetConvar("mail_debug", "false"));
        }
        private Blip CreateBlip(Vector3 position, BlipSprite sprite, BlipColor color, string name, bool shortRange = true, float scale = 0.86f)
        {
            var blip = World.CreateBlip(position);
            blip.Sprite = sprite;
            blip.Color = color;
            blip.IsShortRange = shortRange;
            blip.Name = name;
            blip.Scale = scale;

            return blip;
        }
    }
}
