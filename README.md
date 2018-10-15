# MailDelivery Job

[![Build Status](https://travis-ci.org/d0p3t/MailDelivery.svg?branch=master)](https://travis-ci.org/d0p3t/MailDelivery)

**Current Version:** v1.0.1 
___
**I decided to release this script after a beloved server that I played on shut down. At the time I created this, I was working together with the developers to add some unique functionalities to the server. This was one of them.**

---

## What is MailDelivery?
In short, MailDelivery is a standalone **mail delivery job** that lets players deliver mail all around Blaine County.

A mail delivery man always has his own unique route, designated to one of the three towns: Paleto Bay, Grapeseed, or Sandy Shores, delivering his mail to mailboxes or other interesting places.

Standalone means that it _**does not have built-in ESX or vRP support**_. It does however have events that you can listen for.

Written fully in C#.

---
## Features
* Always receive a **unique route** in one of three towns with 10-12 delivery spots (out of max. 33)
* Marked delivery spots on the map
* Delivery spots are _mailboxes_, _porches_ and _other interesting spots_
* Animation when delivering mail
* Ability to use your own custom van model if you don't like the PostOP Boxville
* **Rental spot** in case the mail man doesn't have his own PostOP Boxville yet!
* Rental vans are spawned in one of the free parking spots.
* Configurable min/max pay per delivery and other settings using **ConVars**
* Adaptable to your own system by using server events for payment and rental costs

___

## Installation & Configuration

### Installation
Installation is simple, download the resource through the link above, add `start maildelivery` to your `server.cfg` and you're ready to go. However, if you would like to configure settings or bind this to your existing economy system, see **Configuration**.

### Configuration
This resource triggers two events for you to conveniently integrate this job into your own system as well as various ConVars to modify settings (like minimum and maximum payments).

There are two server events that you can use to add delivery payments and rental costs to your existing economy system (for example ESX).

**Name:** _"MailDelivery:DeliveryMade"_
**Parameters:** payment(integer)
**Description:** Fired when a delivery has been made. Contains the payment amount between the min and max payment.
**Example:** 
```lua
AddEventHandler("MailDelivery:DeliveryMade", function(payment)
    -- add payment to players bank account
end)
```

**Name:** _"MailDelivery:VanRented"_
**Parameters:** rentalAmount(integer)
**Description:** When renting a van, this event gets triggered with the set rental amount.
**Example:** 
```lua
AddEventHandler("MailDelivery:VanRented", function(rentalAmount)
    -- deduct rentalAmount from players bank account
end)
```

You can configure various aspects of the job using ConVars. Below is the list of all available ConVars and their default values if not set.

**Name:** "mail_min_payment" (_Default: 150_)
**Description:** The minimum payment of a delivery.

**Name:** "mail_max_payment" (_Default: 1000_)
**Description:** The maximum payment of a delivery.

**Name:** "mail_rental_amount" (_Default: 2000_)
**Description:** The rental cost when renting a mail delivery van via the rental spot.

**Name:** "mail_job_cooldown" (_Default: 60000_)
**Description:** Sets the cooldown time between going on/off duty. A security measure to prevent people from getting the optimal route.

**Name:** "mail_van_model" (_Default: "BOXVILLE4"_)
**Description:** Sets the van model required to start the delivery job. Also the model for the rental spot.

**Name:** "mail_debug" (Default:  false)
**Description:** A debug flag for developers (Only set this convar when developing!)

---
## Changelog
**v1.0.1**
* Ability to use your own custom van model
* Moved ConVars to client side due to new FiveM feature

**v1.0.0**
* Initial release

---
## Known Issues
 None as of right now :crossed_fingers:

If you come across anything bugs/issues, please let me know in this topic or create an [Issue](https://github.com/d0p3t/MailDelivery/issues).

---
## Support
If you require assistance, you can ask your question on this topic. Please don't create topics elsewhere or ask me on the FiveM Discord about this resource. Lets keep all questions and comments in one topic, so we are all aware of what's going on.

---
## Credits
Last, but not least, some credits to people that have helped me get into FiveM coding and helped other wise.

Thanks @Mooshe and his stream for the preliminary logic of parking spots and toggling duty (months ago).
Thanks to @Vespura for letting me use the same license :slight_smile: 

___

## Final Words
I hope you enjoy the release. I want this resource to not only be one that community members can use, but also learn from. I feel there is a lack of C# resources as well as source code that developers can learn from.

If you have any requests, concerns, etc. please let me know.

You can modify or edit the code as you like, but you cannot re-release it. Please see the [LICENSE.md](https://github.com/d0p3t/MailDelivery/blob/master/LICENSE.md)