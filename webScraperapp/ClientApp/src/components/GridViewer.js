import { Grid } from "react-visual-grid";
import "react-visual-grid/dist/react-visual-grid.css";
import { BrowserView, MobileView } from 'react-device-detect';

export default function GridViewer({ gridViewerImages }) {

    return (
        <div>
            <BrowserView>
                <Grid
                    images={gridViewerImages}
                    width={1024}
                    height={800}
                  
                    enableDarkMode={false}
                />
            </BrowserView>
            <MobileView>
                <Grid
                    images={gridViewerImages}
                    width={300}
                    height={800}
                    enableDarkMode={false}
                />
            </MobileView>
        </div>
    );
}