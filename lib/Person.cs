using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Blockchain
{
    public class Person {
            public string surname;
            public string bsn;
            public DateTime birthDate;

            public Person(string surname, string bsn, DateTime birthDate){
                this.surname = surname;
                this.bsn = bsn;
                this.birthDate = birthDate;
            }
        }
}