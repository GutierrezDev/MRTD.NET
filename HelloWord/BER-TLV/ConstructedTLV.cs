﻿using HelloWord.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.BER_TLV;

namespace HelloWord.BER_TLV
{
    public class ConstructedTLV
    {
        private readonly IBinary _constructedTLV;
        private readonly IBinary _cachedFirstExistingTLV;

        public ConstructedTLV(IBinary constructedTLV)
        {
            _constructedTLV = constructedTLV;
            _cachedFirstExistingTLV = new BerTLV(_constructedTLV);
        }

        private IBerTLV[] First()
        {
            return new IBerTLV[1] { new BerTLV(_cachedFirstExistingTLV) };
        }

        private IBinary Rest()
        {
            return new Binary(
                        _constructedTLV
                            .Bytes()
                            .Skip(_cachedFirstExistingTLV.Bytes().Length)
                    );
        }

        public IBerTLV[] Data()
        {
            if (new Length(Rest()).IsEmpty())
            {
                return First();
            }
            return First()
                .Concat(
                    new ConstructedTLV(
                        Rest()
                    ).Data()
                ).ToArray();
        }
    }
}