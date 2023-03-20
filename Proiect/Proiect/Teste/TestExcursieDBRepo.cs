﻿using System;

namespace Proiect
{
    public class TestExcursieDBRepo
    {
        [Fact]
        public void TestAdauga()
        {
            ExcursieDBRepo repo = new ExcursieDBRepo();
            Excursie excursie = new Excursie()
            {
                idFirmaTransport = 1,
                idObiectiv = 2,
                ora = new TimeSpan(1234),
                pret = 5,
                nrLocuriTotale = 6,
            };
            repo.adauga(excursie);
        }
    }
}
