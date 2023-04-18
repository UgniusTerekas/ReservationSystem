import { Carousel, Divider, Image } from "antd";
import React, { useState } from "react";
import { GetGallery } from "../../types/gallery";

interface Props {
  gallery: GetGallery[] | undefined;
}

export const EntertainmentGallery = ({ gallery }: Props) => {
  const [currentSlide, setCurrentSlide] = useState<number>(0);

  const handleSlideChange = (current: number) => {
    setCurrentSlide(current);
  };

  console.log(gallery);
  return (
    <React.Fragment>
      <Divider style={{ borderColor: "black", paddingInline: 30 }}>
        Galerija
      </Divider>
      <Carousel
        autoplay={true}
        effect="fade"
        beforeChange={handleSlideChange}
        style={{ width: "100%", textAlign: "center" }}
      >
        {gallery?.map((image, index) => (
          <div
            key={index}
            style={{
              display: "flex",
              alignItems: "center",
              justifyContent: "center",
              height: "100%",
            }}
          >
            <Image
              className="galery"
              src={`https://localhost:7229${image.imageLocation}`}
              alt={`slide-${index}`}
              preview={false}
              style={{
                display: "block",
                margin: "0 auto",
                maxHeight: "100%",
                maxWidth: "100%",
              }}
            />
          </div>
        ))}
      </Carousel>
    </React.Fragment>
  );
};
