﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CryptographyLabrary;

namespace ConsoleApplication1
{
    public partial class EllipticForm : Form
    {
        public EllipticForm()
        {
            InitializeComponent();
        }
        private ECPoint Q = new ECPoint();
        private BInteger d = new BInteger();
        
        private byte[] FromHexStringToByte(string input)
        {
            byte[] data = new byte[input.Length / 2];
            string HexByte = "";
            for (int i = 0; i < data.Length; i++)
            {
                HexByte = input.Substring(i * 2, 2);
                data[i] = Convert.ToByte(HexByte, 16);
            }
            return data;
        }

        private void btnSign_Click(object sender, EventArgs e)
        {
            BInteger p = new BInteger("6277101735386680763835789423207666416083908700390324961279", 10);
            BInteger a = new BInteger("-3", 10);
            BInteger b = new BInteger("64210519e59c80e70fa7e9ab72243049feb8deecc146b9b1", 16);
            byte[] xG = FromHexStringToByte("03188da80eb03090f67cbf20eb43a18800f4ff0afd82ff1012");
            BInteger n = new BInteger("ffffffffffffffffffffffff99def836146bc9b1b4d22831", 16);
            DSGost DS = new DSGost(p, a, b, n, xG);
            d = DS.GenPrivateKey(192);
            Q = DS.GenPublicKey(d);
            GOST hash = new GOST(256);
            byte[] H = hash.GetHash(System.Text.Encoding.Default.GetBytes(textBoxText.Text));
            string sign = DS.SingGen(H, d);
            textBox2Hash.Text = sign;
            bool result = DS.SingVer(H, sign, Q);
        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            BInteger p = new BInteger("6277101735386680763835789423207666416083908700390324961279", 10);
            BInteger a = new BInteger("-3", 10);
            BInteger b = new BInteger("64210519e59c80e70fa7e9ab72243049feb8deecc146b9b1", 16);
            byte[] xG = FromHexStringToByte("03188da80eb03090f67cbf20eb43a18800f4ff0afd82ff1012");
            BInteger n = new BInteger("ffffffffffffffffffffffff99def836146bc9b1b4d22831", 16);
            DSGost DS = new DSGost(p, a, b, n, xG);
            GOST hash = new GOST(256);
            long start = DateTime.Now.Second * 1000 + DateTime.Now.Millisecond;
            byte[] H = hash.GetHash(System.Text.Encoding.Default.GetBytes(textBoxText.Text));
            bool result = DS.SingVer(H, textBox2Hash.Text, Q);
            long finish = DateTime.Now.Second * 1000 + DateTime.Now.Millisecond;
            long time = finish - start;
            if (result)
            {
                labelIsCorrect.ForeColor = Color.Green;
                labelIsCorrect.Text = "Correct";
            }
            else
            {
                labelIsCorrect.ForeColor = Color.Red;
                labelIsCorrect.Text = "Wrong";
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            BInteger p = new BInteger("3623986102229003635907788753683874306021320925534678605086546150450856166624002482588482022271496854025090823603058735163734263822371964987228582907372403", 10);
            BInteger x = new BInteger("1928356944067022849399309401243137598997786635459507974357075491307766592685835441065557681003184874819658004903212332884252335830250729527632383493573274", 10);
            BInteger y = new BInteger("2288728693371972859970012155529478416353562327329506180314497425931102860301572814141997072271708807066593850650334152381857347798885864807605098724013854", 10);
            BInteger a = new BInteger(7);
            BInteger b = new BInteger("1518655069210828534508950034714043154928747527740206436194018823352809982443793732829756914785974674866041605397883677596626326413990136959047435811826396", 10);
            BInteger k = new BInteger("175516356025850499540628279921125280333451031747737791650208144243182057075034446102986750962508909227235866126872473516807810541747529710309879958632945", 10);
            ECPoint P1 = new ECPoint();
            P1.a = a;
            P1.b = b;
            P1.FieldChar = p;
            P1.x = x;
            P1.y = y;
            ECPoint P3 = ECPoint.multiply(k, P1);
            BInteger r = P3.x % p;
            /////// DS forming
            BInteger q = new BInteger("3623986102229003635907788753683874306021320925534678605086546150450856166623969164898305032863068499961404079437936585455865192212970734808812618120619743", 10);
            BInteger d = new BInteger("610081804136373098219538153239847583006845519069531562982388135354890606301782255383608393423372379057665527595116827307025046458837440766121180466875860", 10);
            BInteger E = new BInteger("2897963881682868575562827278553865049173745197871825199562947419041388950970536661109553499954248733088719748844538964641281654463513296973827706272045964", 10);
            BInteger s = ((r * d) + (k * E)) % q;
            //////// DS verification
            BInteger v = E.modInverse(q);
            BInteger z = (s * v) % q;
            BInteger z2 = q + ((-(r * v)) % q);
            BInteger x1 = new BInteger("909546853002536596556690768669830310006929272546556281596372965370312498563182320436892870052842808608262832456858223580713780290717986855863433431150561", 10);
            BInteger y1 = new BInteger(" 2921457203374425620632449734248415455640700823559488705164895837509539134297327397380287741428246088626609329139441895016863758984106326600572476822372076", 10);
            ECPoint Q = new ECPoint();
            Q.a = a;
            Q.b = b;
            Q.x = x1;
            Q.y = y1;
            Q.FieldChar = p;
            ECPoint A = ECPoint.multiply(z, P1);
            ECPoint B = ECPoint.multiply(z2, Q);
            ECPoint C = A + B;
        }

    }
}
