using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading;
using eCommerce.DAL.Model;
using eCommerce.DAL.Model.ComplexTypes;
using eCommerce.DAL.Model.Enums;

namespace eCommerce.DAL
{
    public class VetOfficeInitializer : DropCreateDatabaseAlways<VetOfficeContext>
    {
        protected override void Seed(VetOfficeContext context)
        {
            // Add Schedule
            var schedule = new Schedule
            {
                Id = Guid.NewGuid(),
                ClinicId = 1,
                DateRange = new DateTimeRange
                {
                    Start = DateTime.Now,
                    End = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day)
                }
            };
            context.Schedules.Add(schedule);

            // Add Doctors
            var drSmith = new Doctor {Name = "Dr. Smith"};
            var drWho = new Doctor {Name = "Dr. Who"};
            var drMcDreamy = new Doctor {Name = "Dr. McDreamy"};

            List<Doctor> doctors = new List<Doctor>
            {
                drSmith,
                drWho,
                drMcDreamy
            };
            context.Doctors.AddRange(doctors);

            context.SaveChanges();

            // add Clients

            Func<Client, List<Patient>, Client> funcClientWithPattents = (client, patients) =>
            {
                foreach (var pattent in patients)
                {
                    client.Pattents.Add(pattent);
                }
                return client;
            };

            List<AnimalType> animalTypes = new List<AnimalType>
            {
                new AnimalType {Species = "Dog", Breed = "Newfoundland"},
                new AnimalType {Species = "Dog", Breed = "Jack Russell"},
                new AnimalType {Species = "Dog", Breed = "Corgi"},
                new AnimalType {Species = "Dog", Breed = "Italian Greyhound"},
                new AnimalType {Species = "Cat", Breed = "Mixed"},
                new AnimalType {Species = "Dog", Breed = "Doberman"},
                new AnimalType {Species = "Cat", Breed = "Tabby"},
                new AnimalType {Species = "Dog", Breed = "Mastiff"},
                new AnimalType {Species = "Reptile", Breed = "Python"},
                new AnimalType {Species = "Cat", Breed = "Maltese"},
                new AnimalType {Species = "Dog", Breed = "Mix"},
                new AnimalType {Species = "Dog", Breed = "Dachshund"},
                new AnimalType {Species = "Dog", Breed = "Terrier"},
                new AnimalType {Species = "Dog", Breed = "Labrador"},
                new AnimalType {Species = "Dog", Breed = "Siberian Husky"},
                new AnimalType {Species = "Dog", Breed = "Shih Tzu"},
                new AnimalType {Species = "Lizard", Breed = "Green"},
                new AnimalType {Species = "Cat", Breed = "Tortoiseshell"},
                new AnimalType {Species = "Fish", Breed = "Beta"},
                new AnimalType {Species = "Cat", Breed = "Calico"},
                new AnimalType {Species = "Dog", Breed = "Pug"},
                new AnimalType {Species = "Dog", Breed = "Chihuahua"}

            };

            List<Client> clients = new List<Client>
            {
                funcClientWithPattents(
                    new Client
                    {
                        Contact = new Contact
                        {
                            FullName = new FullName
                            {
                                FirstName = "Steve",
                                LastName = "Smith"
                            },
                            EmailAddress = "client@example.com",
                            CompanyName = "Google Inc.",
                            Phone = "0123456789"
                        },
                        PreferredName = "Steve",
                        Salutation = "Mr.",
                        BirthDay = new DateTime(1990, 6, 9, 10, 0, 0),
                        PreferedDoctor = drSmith
                    },
                    new List<Patient>
                    {
                        new Patient
                        {
                            Gender = Gender.Male,
                            Name = "Sampson",
                            AnimalType = animalTypes[0]
                        }
                    }),
                funcClientWithPattents(
                    new Client
                    {
                        Contact = new Contact
                        {
                            FullName = new FullName
                            {
                                FirstName = "Julia",
                                LastName = "Lerman"
                            },
                            EmailAddress = "client@example.com",
                            CompanyName = "Google Inc.",
                            Phone = "0123456789"
                        },
                        PreferredName = "Julie",
                        Salutation = "Mrs",
                        BirthDay = new DateTime(1990, 6, 9, 10, 0, 0),
                        PreferedDoctor = drMcDreamy
                    },
                    new List<Patient>
                    {
                        new Patient
                        {
                            Gender = Gender.Male,
                            Name = "Sampson",
                            AnimalType = animalTypes[0]
                        }
                    }),
                funcClientWithPattents(
                    new Client
                    {
                        Contact = new Contact
                        {
                            FullName = new FullName
                            {
                                FirstName = "Wes",
                                LastName = "McClure"
                            },
                            EmailAddress = "client@example.com",
                            CompanyName = "Google Inc.",
                            Phone = "0123456789"
                        },
                        PreferredName = "Wes",
                        Salutation = "Mr.",
                        BirthDay = new DateTime(1990, 6, 9, 10, 0, 0),
                        PreferedDoctor = drMcDreamy
                    },
                    new List<Patient>
                    {
                        new Patient
                        {
                            Gender = Gender.Female,
                            Name = "Pax",
                            AnimalType = animalTypes[1],
                            //PreferredDoctor = drMcDreamy
                        }
                    }),
                funcClientWithPattents(
                    new Client
                    {
                        Contact = new Contact
                        {
                            FullName = new FullName
                            {
                                FirstName = "Andrew",
                                LastName = "Mallett"
                            },
                            EmailAddress = "client@example.com",
                            CompanyName = "Google Inc.",
                            Phone = "0123456789"
                        },
                        PreferredName = "Andrew",
                        Salutation = "Mr.",
                        BirthDay = new DateTime(1990, 6, 9, 10, 0, 0),
                        PreferedDoctor = drMcDreamy
                    },
                    new List<Patient>
                    {
                        new Patient
                        {
                            Gender = Gender.Male,
                            Name = "Barney",
                            AnimalType = animalTypes[2],
                            //PreferredDoctor = drMcDreamy
                        }
                    }),
                funcClientWithPattents(
                    new Client
                    {
                        Contact = new Contact
                        {
                            FullName = new FullName
                            {
                                FirstName = "Brian",
                                LastName = "Lagunas"
                            },
                            EmailAddress = "client@example.com",
                            CompanyName = "Google Inc.",
                            Phone = "0123456789"
                        },
                        PreferredName = "Brian",
                        Salutation = "Mr.",
                        BirthDay = new DateTime(1990, 6, 9, 10, 0, 0),
                        PreferedDoctor = drWho
                    },
                    new List<Patient>
                    {
                        new Patient
                        {
                            Gender = Gender.Male,
                            Name = "Rocky",
                            AnimalType = animalTypes[3],
                            //PreferredDoctor = drWho
                        }
                    }),
                funcClientWithPattents(
                    new Client
                    {
                        Contact = new Contact
                        {
                            FullName = new FullName
                            {
                                FirstName = "Corey",
                                LastName = "Haines"
                            },
                            EmailAddress = "client@example.com",
                            CompanyName = "Google Inc.",
                            Phone = "0123456789"
                        },
                        PreferredName = "Corey",
                        Salutation = "Mr.",
                        BirthDay = new DateTime(1990, 6, 9, 10, 0, 0),
                        PreferedDoctor = drWho
                    },
                    new List<Patient>
                    {
                        new Patient
                        {
                            Gender = Gender.Male,
                            Name = "Zak",
                            AnimalType = animalTypes[4],
                            //PreferredDoctor = drWho
                        }
                    }),
                funcClientWithPattents(
                    new Client
                    {
                        Contact = new Contact
                        {
                            FullName = new FullName
                            {
                                FirstName = "Patrick",
                                LastName = "Hynds"
                            },
                            EmailAddress = "client@example.com",
                            CompanyName = "Google Inc.",
                            Phone = "0123456789"
                        },
                        PreferredName = "Patrick",
                        Salutation = "Mr.",
                        BirthDay = new DateTime(1990, 6, 9, 10, 0, 0),
                        PreferedDoctor = drWho
                    },
                    new List<Patient>
                    {
                        new Patient
                        {
                            Gender = Gender.Male,
                            Name = "Anubis",
                            AnimalType = animalTypes[5],
                            //PreferredDoctor = drWho
                        }
                    }),
                funcClientWithPattents(
                    new Client
                    {
                        Contact = new Contact
                        {
                            FullName = new FullName
                            {
                                FirstName = "Lars",
                                LastName = "Klint"
                            },
                            EmailAddress = "client@example.com",
                            CompanyName = "Google Inc.",
                            Phone = "0123456789"
                        },
                        PreferredName = "Lars",
                        Salutation = "Mr.",
                        BirthDay = new DateTime(1990, 6, 9, 10, 0, 0),
                        PreferedDoctor = drWho
                    },
                    new List<Patient>
                    {
                        new Patient
                        {
                            Gender = Gender.Male,
                            Name = "Boots",
                            AnimalType = animalTypes[6],
                            //PreferredDoctor = drWho
                        }
                    }),
                funcClientWithPattents(
                    new Client
                    {
                        Contact = new Contact
                        {
                            FullName = new FullName
                            {
                                FirstName = "Joe",
                                LastName = "Eames"
                            },
                            EmailAddress = "client@example.com",
                            CompanyName = "Google Inc.",
                            Phone = "0123456789"
                        },
                        PreferredName = "Joe",
                        Salutation = "Mr.",
                        BirthDay = new DateTime(1990, 6, 9, 10, 0, 0),
                        PreferedDoctor = drWho
                    },
                    new List<Patient>
                    {
                        new Patient
                        {
                            Gender = Gender.Male,
                            Name = "Corde",
                            AnimalType = animalTypes[7],
                            //PreferredDoctor = drWho
                        }
                    }),
                funcClientWithPattents(
                    new Client
                    {
                        Contact = new Contact
                        {
                            FullName = new FullName
                            {
                                FirstName = "Joe",
                                LastName = "Kunk"
                            },
                            EmailAddress = "client@example.com",
                            CompanyName = "Google Inc.",
                            Phone = "0123456789"
                        },
                        PreferredName = "Joe",
                        Salutation = "Mr.",
                        BirthDay = new DateTime(1990, 6, 9, 10, 0, 0),
                        PreferedDoctor = drWho
                    },
                    new List<Patient>
                    {
                        new Patient
                        {
                            Gender = Gender.Male,
                            Name = "Wedgie",
                            AnimalType = animalTypes[8],
                            //PreferredDoctor = drWho
                        }
                    }),
                funcClientWithPattents(
                    new Client
                    {
                        Contact = new Contact
                        {
                            FullName = new FullName
                            {
                                FirstName = "Ross",
                                LastName = "Bagurdes"
                            },
                            EmailAddress = "client@example.com",
                            CompanyName = "Google Inc.",
                            Phone = "0123456789"
                        },
                        PreferredName = "Ross",
                        Salutation = "Mr.",
                        BirthDay = new DateTime(1990, 6, 9, 10, 0, 0),
                        PreferedDoctor = drWho
                    },
                    new List<Patient>
                    {
                        new Patient
                        {
                            Gender = Gender.Male,
                            Name = "Indiana Jones",
                            AnimalType = animalTypes[6],
                            //PreferredDoctor = drWho
                        }
                    }),
                funcClientWithPattents(
                    new Client
                    {
                        Contact = new Contact
                        {
                            FullName = new FullName
                            {
                                FirstName = "Patrick",
                                LastName = "Neborg"
                            },
                            EmailAddress = "client@example.com",
                            CompanyName = "Google Inc.",
                            Phone = "0123456789"
                        },
                        PreferredName = "Patrick",
                        Salutation = "Mr.",
                        BirthDay = new DateTime(1990, 6, 9, 10, 0, 0),
                        PreferedDoctor = drWho
                    },
                    new List<Patient>
                    {
                        new Patient
                        {
                            Gender = Gender.Male,
                            Name = "Sugar",
                            AnimalType = animalTypes[9],
                            //PreferredDoctor = drWho
                        }
                    }),
                funcClientWithPattents(
                    new Client
                    {
                        Contact = new Contact
                        {
                            FullName = new FullName
                            {
                                FirstName = "David",
                                LastName = "Mann"
                            },
                            EmailAddress = "client@example.com",
                            CompanyName = "Google Inc.",
                            Phone = "0123456789"
                        },
                        PreferredName = "David",
                        Salutation = "Mr.",
                        BirthDay = new DateTime(1990, 6, 9, 10, 0, 0),
                        PreferedDoctor = drWho
                    },
                    new List<Patient>
                    {
                        new Patient
                        {
                            Gender = Gender.Male,
                            Name = "Piper",
                            AnimalType = animalTypes[10],
                            //PreferredDoctor = drWho
                        }
                    }),

                funcClientWithPattents(
                    new Client
                    {
                        Contact = new Contact
                        {
                            FullName = new FullName
                            {
                                FirstName = "Peter",
                                LastName = "Mourfield"
                            },
                            EmailAddress = "client@example.com",
                            CompanyName = "Google Inc.",
                            Phone = "0123456789"
                        },
                        PreferredName = "Peter",
                        Salutation = "Mr.",
                        BirthDay = new DateTime(1990, 6, 9, 10, 0, 0),
                        PreferedDoctor = drWho
                    },
                    new List<Patient>
                    {
                        new Patient
                        {
                            Gender = Gender.Male,
                            Name = "Finley",
                            AnimalType = animalTypes[11],
                            //PreferredDoctor = drWho
                        }
                    }),
                funcClientWithPattents(
                    new Client
                    {
                        Contact = new Contact
                        {
                            FullName = new FullName
                            {
                                FirstName = "Keith",
                                LastName = "Harvey"
                            },
                            EmailAddress = "client@example.com",
                            CompanyName = "Google Inc.",
                            Phone = "0123456789"
                        },
                        PreferredName = "Keith",
                        Salutation = "Mr.",
                        BirthDay = new DateTime(1990, 6, 9, 10, 0, 0),
                        PreferedDoctor = drWho
                    },
                    new List<Patient>
                    {
                        new Patient
                        {
                            Gender = Gender.Male,
                            Name = "Bella",
                            AnimalType = animalTypes[12],
                            //PreferredDoctor = drWho
                        }
                    }),
                funcClientWithPattents(
                    new Client
                    {
                        Contact = new Contact
                        {
                            FullName = new FullName
                            {
                                FirstName = "Andrew",
                                LastName = "Connell"
                            },
                            EmailAddress = "client@example.com",
                            CompanyName = "Google Inc.",
                            Phone = "0123456789"
                        },
                        PreferredName = "Andrew",
                        Salutation = "Mr.",
                        BirthDay = new DateTime(1990, 6, 9, 10, 0, 0),
                        PreferedDoctor = drWho
                    },
                    new List<Patient>
                    {
                        new Patient
                        {
                            Gender = Gender.Male,
                            Name = "Luabelle",
                            AnimalType = animalTypes[13],
                            //PreferredDoctor = drWho
                        },
                        //new Patient
                        //{
                        //    Gender = Gender.Female,
                        //    Name = "Sadie",
                        //    AnimalType = new AnimalType {Species = "Dog", Breed = "Mix"},
                        //    //PreferredDoctor = drWho
                        //}
                    }),
                funcClientWithPattents(
                    new Client
                    {
                        Contact = new Contact
                        {
                            FullName = new FullName
                            {
                                FirstName = "Julie",
                                LastName = "Yack"
                            },
                            EmailAddress = "client@example.com",
                            CompanyName = "Google Inc.",
                            Phone = "0123456789"
                        },
                        PreferredName = "Julie",
                        Salutation = "Mr.",
                        BirthDay = new DateTime(1990, 6, 9, 10, 0, 0),
                        PreferedDoctor = drWho
                    },
                    new List<Patient>
                    {
                        new Patient
                        {
                            Gender = Gender.Male,
                            Name = "Ruske",
                            AnimalType = animalTypes[14],
                            //PreferredDoctor = drWho
                        },
                        new Patient
                        {
                            Gender = Gender.Female,
                            Name = "Ginger",
                            AnimalType = animalTypes[15],
                            //PreferredDoctor = drWho
                        },
                        new Patient
                        {
                            Gender = Gender.Female,
                            Name = "Lizzie",
                            AnimalType = animalTypes[16],
                            //PreferredDoctor = drWho
                        }
                    }),
                funcClientWithPattents(
                    new Client
                    {
                        Contact = new Contact
                        {
                            FullName = new FullName
                            {
                                FirstName = "Jesse",
                                LastName = "Liberty"
                            },
                            EmailAddress = "client@example.com",
                            CompanyName = "Google Inc.",
                            Phone = "0123456789"
                        },
                        PreferredName = "Jesse",
                        Salutation = "Mr.",
                        BirthDay = new DateTime(1990, 6, 9, 10, 0, 0),
                        PreferedDoctor = drWho
                    },
                    new List<Patient>
                    {
                        new Patient
                        {
                            Gender = Gender.Male,
                            Name = "Jesse",
                            AnimalType = animalTypes[10],
                            //PreferredDoctor = drWho
                        },
                        //new Patient
                        //{
                        //    Gender = Gender.Female,
                        //    Name = "Allegra",
                        //    AnimalType = new AnimalType {Species = "Cat", Breed = "Calico"},
                        //    //PreferredDoctor = drWho
                        //},
                        new Patient
                        {
                            Gender = Gender.Female,
                            Name = "Misty",
                            AnimalType = animalTypes[17],
                            //PreferredDoctor = drWho
                        }
                    }),
                funcClientWithPattents(
                    new Client
                    {
                        Contact = new Contact
                        {
                            FullName = new FullName
                            {
                                FirstName = "Tyler",
                                LastName = "Young"
                            },
                            EmailAddress = "client@example.com",
                            CompanyName = "Google Inc.",
                            Phone = "0123456789"
                        },
                        PreferredName = "Tyler",
                        Salutation = "Mr.",
                        BirthDay = new DateTime(1990, 6, 9, 10, 0, 0),
                        PreferedDoctor = drWho
                    },
                    new List<Patient>
                    {
                        new Patient
                        {
                            Gender = Gender.Male,
                            Name = "Willie",
                            AnimalType = animalTypes[11],
                            //PreferredDoctor = drWho
                        },
                        new Patient
                        {
                            Gender = Gender.Female,
                            Name = "JoeFish",
                            AnimalType = animalTypes[18],
                            //PreferredDoctor = drWho
                        },
                        //new Patient
                        //{
                        //    Gender = Gender.Female,
                        //    Name = "Fabian",
                        //    AnimalType = new AnimalType {Species = "Cat", Breed = "Mixed"},
                        //    //PreferredDoctor = drWho
                        //}
                    }),

                funcClientWithPattents(
                    new Client
                    {
                        Contact = new Contact
                        {
                            FullName = new FullName
                            {
                                FirstName = "Michael",
                                LastName = "Perry"
                            },
                            EmailAddress = "client@example.com",
                            CompanyName = "Google Inc.",
                            Phone = "0123456789"
                        },
                        PreferredName = "Michael",
                        Salutation = "Mr.",
                        BirthDay = new DateTime(1990, 6, 9, 10, 0, 0),
                        PreferedDoctor = drWho
                    },
                    new List<Patient>
                    {
                        new Patient
                        {
                            Gender = Gender.Male,
                            Name = "Callie",
                            AnimalType = animalTypes[19],
                            //PreferredDoctor = drWho
                        },
                        new Patient
                        {
                            Gender = Gender.Female,
                            Name = "Radar",
                            AnimalType = animalTypes[20],
                            //PreferredDoctor = drWho
                        },
                        new Patient
                        {
                            Gender = Gender.Female,
                            Name = "Tinkerbell",
                            AnimalType = animalTypes[21],
                            //PreferredDoctor = drWho
                        }
                    })
            };
            context.Clients.AddRange(clients);

            // add Rooms
            List<Room> rooms = new List<Room>
            {
                new Room {Name = "Exam Room 1"},
                new Room {Name = "Exam Room 2"},
                new Room {Name = "Exam Room 3"},
                new Room {Name = "Exam Room 4"},
                new Room {Name = "Exam Room 5"},
                new Room {Name = "Exam Room 6"}
            };
            context.Rooms.AddRange(rooms);

            // add Types of Appoinment
            List<AppointmentType> appointmentTypes = new List<AppointmentType>
            {
                new AppointmentType
                {
                    Code = "WE",
                    Name = "Wellness Exam",
                    Duration = 30
                },
                new AppointmentType
                {
                    Code = "DE",
                    Name = "Diagnostic Exam",
                    Duration = 60
                },
                new AppointmentType
                {
                    Code = "NT",
                    Name = "Nail Trim",
                    Duration = 30
                }
            };
            context.AppointmentTypes.AddRange(appointmentTypes);

            //add Appoinments
            List<Appointment> appointments = new List<Appointment>
            {
                new Appointment
                {
                    AppointmentType = appointmentTypes[0],
                    Schedule = schedule,
                    Client = clients[0],
                    Doctor = doctors[0],
                    Id = Guid.NewGuid(),
                    Patient = clients[0].Pattents[0],
                    Room = rooms[0],
                    DateRange = new DateTimeRange
                    {
                        Start = new DateTime(2014, 6, 9, 10, 0, 0),
                        End = new DateTime(2016, 6, 9, 11, 0, 0)
                    },
                    Title = "(WE) Darwin - Steve Smith"
                },

                new Appointment
                {
                    AppointmentType = appointmentTypes[1],
                    Schedule = schedule,
                    Client = clients[1],
                    Doctor = doctors[1],
                    Id = Guid.NewGuid(),
                    Patient = clients[1].Pattents[0],
                    Room = rooms[1],
                    DateRange = new DateTimeRange
                    {
                        Start = new DateTime(2014, 6, 9, 10, 0, 0),
                        End = new DateTime(2016, 6, 9, 11, 0, 0)
                    },
                    Title = "(DE) Sampson - Julie Lerman"
                },

                new Appointment
                {
                    AppointmentType = appointmentTypes[1],
                    Schedule = schedule,
                    Client = clients[2],
                    Doctor = doctors[2],
                    Id = Guid.NewGuid(),
                    Patient = clients[2].Pattents[0],
                    Room = rooms[2],
                    DateRange = new DateTimeRange
                    {
                        Start = new DateTime(2014, 6, 9, 10, 0, 0),
                        End = new DateTime(2016, 6, 9, 11, 0, 0)
                    },
                    Title = "(DE) Pax - Wes McClure"
                },

                new Appointment
                {
                    AppointmentType = appointmentTypes[1],
                    Schedule = schedule,
                    Client = clients[3],
                    Doctor = doctors[2],
                    Id = Guid.NewGuid(),
                    Patient = clients[3].Pattents[0],
                    Room = rooms[3],
                    DateRange = new DateTimeRange
                    {
                        Start = new DateTime(2014, 6, 9, 10, 0, 0),
                        End = new DateTime(2016, 6, 9, 11, 0, 0)
                    },
                    Title = "(DE) Charlie - Jesse Liberty"
                },
            };
            context.Appointments.AddRange(appointments);

            context.SaveChanges();


            base.Seed(context);
        }

    }
}