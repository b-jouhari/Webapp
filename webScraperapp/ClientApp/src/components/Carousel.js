
import React, { useState } from 'react';
import Slider from "react-slick";

import  "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";
export default function Carousel({ data }) {

    const [nav1, setNav1] = useState(null);
    const [nav2, setNav2] = useState(null);



    return (
        <div>
          <Slider asNavFor={nav2} ref={(slider1) => setNav1(slider1)}>
                {data.map((item, index) => (

                    <div className="slick1-div-container" key={index} >
                        <img className="slick1-div-container-img"
                            src={item}
                            alt={item}
                        />
                    </div>
                ))}
                </Slider>
                
            <Slider
                asNavFor={nav1}
                ref={(slider2) => setNav2(slider2)}
                slidesToShow={10}
                swipeToSlide={true}
                focusOnSelect={true}
            >
                {data.map((item, index) => (

                    <div className="slick2-div-container"  key={index} >
                        <img className="slick2-div-container-img" 
                            src={item}
                            alt={item}
                        />
                    </div>
                ))}
            </Slider>



        </div>
    )
}

 