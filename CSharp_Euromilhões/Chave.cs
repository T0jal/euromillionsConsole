using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chave
{
    public class Chave
    {
        #region Attributes
        private int[] _numbs = new int[5];
        private int[] _stars = new int[2];
        #endregion

        #region Constructors
        public Chave()
        {
            Random rnd = new Random();
            _numbs[0] = rnd.Next(1, 51);
            _numbs[1] = rnd.Next(1, 51);
            _numbs[2] = rnd.Next(1, 51);
            _numbs[3] = rnd.Next(1, 51);
            _numbs[4] = rnd.Next(1, 51);
            _stars[0] = rnd.Next(1, 13);
            _stars[1] = rnd.Next(1, 13);
        }
        public Chave(int a, int b, int c, int d, int e, int f, int g) // I didn't put " this() " here as I want the program to give the value 0
                                                                      // to the atribute if it does not pass the validation of the property
                                                                      // validateKey()                                                                     
        {
            Number1 = a;
            Number2 = b;
            Number3 = c;
            Number4 = d;
            Number5 = e;
            Star1   = f;
            Star2   = g;
        }
        public Chave(Chave c) { Number1 = c._numbs[0]; Number2 = c._numbs[1]; Number3 = c._numbs[2]; Number4 = c._numbs[3]; Number5 = c._numbs[4]; 
                                Star1 = c._stars[0]; Star2 = c._stars[1]; }
        #endregion

        #region Properties
        public int Number1
        {
            get { return _numbs[0]; }
            set
            {
                if (value > 0 && value < 51)
                {
                    _numbs[0] = value;
                }
            }
        }

        public int Number2
        {
            get { return _numbs[1]; }
            set
            {
                if (value > 0 && value < 51)
                {
                    _numbs[1] = value;
                }
            }
        }

        public int Number3
        {
            get { return _numbs[2]; }
            set
            {
                if (value > 0 && value < 51)
                {
                    _numbs[2] = value;
                }
            }
        }

        public int Number4
        {
            get { return _numbs[3]; }
            set
            {
                if (value > 0 && value < 51)
                {
                    _numbs[3] = value;
                }
            }
        }

        public int Number5
        {
            get { return _numbs[4]; }
            set
            {
                if (value > 0 && value < 51)
                {
                    _numbs[4] = value;
                }
            }
        }

        public int Star1
        {
            get { return _stars[0]; }
            set
            {
                if (value > 0 && value < 13)
                {
                    _stars[0] = value;
                }
            }
        }

        public int Star2
        {
            get { return _stars[1]; }
            set
            {
                if (value > 0 && value < 13)
                {
                    _stars[1] = value;
                }
            }
        }
        #endregion

        #region Methods
        
        public bool validateKey()
        {
            if (Number1 != 0 && Number2 != 0 && Number3 != 0 && Number4 != 0 && Number5 != 0 && Star1 != 0 && Star2 != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int play(Chave rndKey)
        {
            int cont = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int k = 0; k < i+1; k++)
                {
                    if (_numbs[k]==rndKey._numbs[i])
                    {
                        cont++;
                        break;
                    }
                }
            }
            for (int i = 0; i < 2; i++)
            {
                for (int k = 0; k < i + 1; k++)
                {
                    if (_stars[i] == rndKey._stars[k])
                    {
                        cont++;
                        break;
                    }
                }
            }
            return cont;
        }
        public void orderKey()
        {
            Array.Sort(_numbs);
            Array.Sort(_stars);
        }

        public override string ToString()
        {
            return $"{Number1},{Number2},{Number3},{Number4},{Number5},{Star1},{Star2}";
        }

        public string printToConsole()
        {
            return $"Numbers: {Number1} | {Number2} | {Number3} | {Number4} | {Number5}\nStars:   {Star1} | {Star2}\n";
        }
        #endregion
    }
}