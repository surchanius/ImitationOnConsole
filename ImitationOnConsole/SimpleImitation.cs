using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImitationOnConsole
{
    class SimpleImitation
    {
        // u=[0,1]
        // v=[0,1]
        double сLeader, сFollower;
        int numberOfParts;
        double homeostasisMax, homeostasisMin, alphaHomeostasisParametr, bettaHomeostasisParametr;
        double eps;//точность

        public SimpleImitation(double сLeader, double сFollower, int numberOfParts, double homeostasisMin, double homeostasisMax, double alphaHomeostasisParametr, double bettaHomeostasisParametr)
        {
            this.сLeader = сLeader;
            this.сFollower = сFollower;
            this.numberOfParts = numberOfParts;
            this.homeostasisMin = homeostasisMin;
            this.homeostasisMax = homeostasisMax;
            this.alphaHomeostasisParametr = alphaHomeostasisParametr;
            this.bettaHomeostasisParametr = bettaHomeostasisParametr;
        }


        public double fLeaderPayoffFunction(double uLeaderStrategy, double vFollowerStrategy) //функция выйгрыша ведущего
        {
            return сLeader * Math.Sqrt(uLeaderStrategy) / (1 + vFollowerStrategy);
        }
        public double gFollowerPayoffFunction(double uLeaderStrategy, double vFollowerStrategy) //функция выйгрыша ведомого
        {
            return сFollower * Math.Pow(vFollowerStrategy, 1.5) * (1 - uLeaderStrategy);
        }
        public bool HomeostasisSatisfied(double vFollowerStrategy)
        {
            double h = alphaHomeostasisParametr * vFollowerStrategy + bettaHomeostasisParametr;
            return h<homeostasisMax && h>homeostasisMin;
        }
        public Tuple<double,double> FindStackelbergEquilibrium()//находит равновесие по Штакельбергу 
        {
            double uLeaderEqStrategy=-10000, vFollowerEqStrategy=-10000;
            double fLeaderEqPayoff = -10000;
            for (int i = 0; i <= numberOfParts; i++)//пробегаем 
            {
                double uLeaderStrategy = (double)i / numberOfParts;
                double  gFollowerPayoff= -10000;
                double vBestResponse = -10000;
                for (int j = 0; j <= numberOfParts; j++) //находим лучший ответ 
                {
                    double vFollowerStrategy = (double)j/ numberOfParts;
                    double newFollowerPayoff = gFollowerPayoffFunction(uLeaderStrategy, vFollowerStrategy);
                    if (gFollowerPayoff < newFollowerPayoff)
                    {
                        gFollowerPayoff = newFollowerPayoff;
                        vBestResponse = vFollowerStrategy;
                    }
                }
                if (HomeostasisSatisfied(vBestResponse))
                {
                    double newLeaderPayoff = fLeaderPayoffFunction(uLeaderStrategy, vBestResponse);
                    if (fLeaderEqPayoff < newLeaderPayoff)
                    {
                        fLeaderEqPayoff = newLeaderPayoff;
                        uLeaderEqStrategy = uLeaderStrategy;                        
                        vFollowerEqStrategy=vBestResponse;
                    }
                }
            }
            if (uLeaderEqStrategy == -10000)
            {
                Console.WriteLine("Ни одна стратегия ведомого не удоволетворяет условию гомеостаза");
            }
            return new Tuple<double, double> (uLeaderEqStrategy, vFollowerEqStrategy);
        }
    }
}
