import { Carousel, Divider, Image } from "antd";
import React, { useState } from "react";

export const EntertainmentGallery = () => {
  const images: string[] = [
    "https://picsum.photos/600/600",
    "https://picsum.photos/600/600",
    "https://media.istockphoto.com/id/466187907/photo/landscape-aerial-view.jpg?s=612x612&w=0&k=20&c=4DAsAkuuXNxBtLWlL-dpQxFKoKsq3zpr6756Ke4eaao=",
    "https://picsum.photos/600/600",
  ];

  const [currentSlide, setCurrentSlide] = useState<number>(0);

  const handleSlideChange = (current: number) => {
    setCurrentSlide(current);
  };

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
        {images.map((image, index) => (
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
              src={image}
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
