using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Blockchain
{
    public class Company {
            public string name;
            public string publicKey;
            public List<Permission> permissions;

            public List<Permission> GetAllPermissions (){
                return this.permissions;
            }
            public List<Permission> GetTruePermissions (){
                List<Permission> permissions = new List<Permission>();
                foreach(Permission p in this.permissions){
                    if(p.value){
                        permissions.Add(p);
                    }
                }
                return(permissions);
                // if(this.aanhoudingen){permissions.Add("aanhoudingen");}
                // if(this.antecedenten){permissions.Add("antecedenten");}
                // if(this.heeftISDMaatregel){permissions.Add("heeftISDMaatregel");}
                // if(this.heeftIdBewijs){permissions.Add("heeftIdBewijs");}
                // if(this.heeftLopendTraject){permissions.Add("heeftLopendTraject");}
                // if(this.heeftOnderzoekRad){permissions.Add("heeftOnderzoekRad");}
                // if(this.heeftUitkering){permissions.Add("heeftUitkering");}
                // if(this.laatsteGesprek){permissions.Add("laatsteGesprek");}
                // if(this.lopendeDossiers){permissions.Add("lopendeDossiers");}
                // if(this.meldingenRad){permissions.Add("meldingenRad");}
                // if(this.sepots){permissions.Add("sepots");}
                // if(this.zitInGroepsAanpak){permissions.Add("zitInGroepsAanpak");}
                // return permissions;
            }
    }

    public class Permission {
        public string name;
        public bool value;
            // public bool antecedenten; 
            // public bool aanhoudingen;
            // public bool heeftISDMaatregel;
            // public bool heeftOnderzoekRad;
            // public bool sepots;
            // public bool lopendeDossiers; 
            // public bool heeftUitkering;
            // public bool meldingenRad;
            // public bool zitInGroepsAanpak;
            // public bool heeftIdBewijs;
            // public bool heeftLopendTraject;
            // public bool laatsteGesprek;
        }
}