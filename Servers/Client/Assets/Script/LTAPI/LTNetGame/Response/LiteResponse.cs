using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LTNet;
using UnityEngine;

class LiteResponse : Response {

    public override void Deserialize(DataStream reader) {
        base.Deserialize(reader);

        uint msgCode = reader.ReadInt32();

        byte dataModNum = reader.ReadByte();

        while(dataModNum > 0) {
            // Tryparse
            HandlerModule(reader);
            dataModNum--;
        }
    }

    private void HandlerModule(DataStream reader) {
        ushort dataModCode = reader.ReadInt16();
        Debug.Log(dataModCode);
        switch(dataModCode) {
            case NetProtocols.NTC_DTCD_ENTER_IN_TEST:
                Debug.Log(reader.ReadString16());
                break;
        }
    }
}

