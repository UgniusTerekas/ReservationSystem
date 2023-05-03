import { Carousel, Divider, Image } from "antd";
import React, { useState } from "react";
import { GetGallery } from "../../types/gallery";

interface Props {
  gallery: GetGallery[] | undefined;
}

export const EntertainmentGallery = ({ gallery }: Props) => {
  const [currentSlide, setCurrentSlide] = useState<number>(0);
  let filteredImages;

  const handleSlideChange = (current: number) => {
    setCurrentSlide(current);
  };

  if (gallery) {
    filteredImages = gallery.filter(
      (image, index, self) =>
        index === self.findIndex((i) => i.imageName === image.imageName)
    );
  }

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
        {filteredImages?.map((image, index) => (
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
                maxHeight: "70%",
                maxWidth: "70%",
              }}
            />
          </div>
        ))}
      </Carousel>
    </React.Fragment>
  );
};
