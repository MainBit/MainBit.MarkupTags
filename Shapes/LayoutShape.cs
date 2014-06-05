using Orchard.DisplayManagement.Descriptors;
using Orchard.UI.Zones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainBit.MarkupTags.Shapes
{
    public class LayoutShape : IShapeTableProvider {

        public void Discover(ShapeTableBuilder builder)
        {
            builder.Describe("Layout")
                .OnCreated(created =>
                {
                    var layout = created.Shape;

                    layout.BeforeBody = created.New.DocumentZone(ZoneName: "BeforeBody");
                });
        }
    }
}