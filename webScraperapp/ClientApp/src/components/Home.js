import React, { useState, useEffect, useMemo, useCallback } from 'react';
import  TextInsider  from './TextInsider';
import  GridViewer  from './GridViewer';
const isValidUrl = (url) => {
    const regex = /[-a-zA-Z0-9@:%._\\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\\+.~#?&//=]*)?/i;
    return regex.test(url);
};

 
export default function Home() {

    const [scrapedImageWithAltTextData, setScrapedImageWithAltTextData] = useState([]);
    const [barData, setBarData] = useState([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);
    const [url, setUrl] = useState('');

    const fetchWebScraperData = useCallback(async () => {
        try {
            setLoading(true); 

            const scrapedImageWithAltTextEndPoint = 'webscraper/GetWebScrapedImagesWithAltText/?url=' + url;
            const scrapedImageWithAltTextResponse = await fetch(scrapedImageWithAltTextEndPoint);
            const scrapedImageWithAltTextData = await scrapedImageWithAltTextResponse.json();

            const scraperInsightsEndPoint = 'webscraper/GetWebScraperInsights/?url=' + url;
            const scraperInsightsResponse = await fetch(scraperInsightsEndPoint);
            const scraperInsightsData = await scraperInsightsResponse.json();
            const words = scraperInsightsData.words;

           
            const labels = words.map((word) => word.key);

            const barDataValues = words.map((word) => word.value);

            const dataInsight = {
                labels,
                datasets: [
                    {
                        label: '# of Top Words',
                        data: barDataValues,
                        borderColor: 'rgb(255, 99, 132)',
                        backgroundColor: 'rgba(255, 99, 132, 0.5)',
                    },
                ],
            }

            setBarData(dataInsight);
            setScrapedImageWithAltTextData(scrapedImageWithAltTextData);
            setLoading(false);
        } catch (error) {
            setError(error);
            setLoading(false);
        }
    }, [url]);

    if (loading) {
        return <div>Loading...</div>;
    }

    if (error) {
        return <div>Error: {error.message}</div>;
    }

    return (
        <div className="mainContainer">
            
            <label>
                Url:
                <input id="url" value={url} onChange={(event) => setUrl(event.target.value)} />
            </label>

            <button className="btn btn-primary" onClick={fetchWebScraperData} disabled={!isValidUrl(url)}>
                Start Scaping
            </button>

            <div className="componentContainer">

                {(scrapedImageWithAltTextData === undefined || scrapedImageWithAltTextData.length === 0) ? '' :
                    <GridViewer gridViewerImages={scrapedImageWithAltTextData} />}
                {(barData === undefined || barData.length === 0) ? '' : <TextInsider barData={barData} />}
            </div>
        </div>
    );
}
