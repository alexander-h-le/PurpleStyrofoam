using PurpleStyrofoam.Rendering.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleStyrofoam.Helpers
{
    public class TestHelper
    {
        public static Dialogue[] GetTestDialogues()
        {
            return new Dialogue[] {
                new Dialogue(PlayerManager.basePlayerSpriteName, 
                "Lorem Ipsum", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Feugiat vivamus at augue eget arcu. Pretium nibh ipsum consequat nisl vel pretium lectus quam id. Phasellus egestas tellus rutrum tellus pellentesque eu tincidunt tortor. Integer quis auctor elit sed vulputate mi sit amet. Nunc sed id semper risus in hendrerit gravida. Placerat orci nulla pellentesque dignissim enim sit amet venenatis urna. Diam sollicitudin tempor id eu nisl nunc. Eu turpis egestas pretium aenean pharetra magna ac placerat. Consectetur lorem donec massa sapien. Proin sed libero enim sed faucibus turpis in eu mi. Rhoncus dolor purus non enim. Viverra suspendisse potenti nullam ac. Tellus at urna condimentum mattis. Blandit libero volutpat sed cras. Pulvinar etiam non quam lacus suspendisse faucibus interdum posuere lorem. Blandit aliquam etiam erat velit. Ut pharetra sit amet aliquam id diam.", DIALOGUELOCATION.RIGHT)
            };
        }
    }
}
