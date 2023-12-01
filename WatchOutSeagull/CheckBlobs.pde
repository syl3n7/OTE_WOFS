/**
 * This function checks for blobs in the video feed based on a specified color threshold.
 * It iterates through every pixel in the video feed and compares the color of each pixel
 * with the specified track color. If the color difference is below the threshold, a new blob
 * is created or an existing blob is updated. Blobs that do not meet the minimum size requirement
 * are removed. Finally, the function displays the blobs on the screen.
 */

void checkBlobs(){

    video.loadPixels();
    image(video, height/2, width/2);

    ArrayList<Blob> currentBlobs = new ArrayList<Blob>();

    // Begin loop to walk through every pixel in the video feed
    for (int x = 0; x < video.width; x++ ) {
        for (int y = 0; y < video.height; y++ ) {
            int loc = x + y * video.width;
            // What is current color
            color currentColor = video.pixels[loc];
            float r1 = red(currentColor);
            float g1 = green(currentColor);
            float b1 = blue(currentColor);
            float r2 = red(trackColor);
            float g2 = green(trackColor);
            float b2 = blue(trackColor);

            float d = distSq(r1, g1, b1, r2, g2, b2); 

            if (d < threshold*threshold) {

                boolean found = false;
                for (Blob b : currentBlobs) {
                    if (b.isNear(x, y)) {
                        b.add(x, y);
                        found = true;
                        break;
                    }
                }

                if (!found) {
                    Blob b = new Blob(x, y);
                    currentBlobs.add(b);
                }
            }
        }
    }

    for (int i = currentBlobs.size()-1; i >= 0; i--) {
        if (currentBlobs.get(i).size() < 500) {
            currentBlobs.remove(i);
        }
    }

    // There are no blobs!
    if (blobs.isEmpty() && currentBlobs.size() > 0) {
        println("Adding blobs!");
        for (Blob b : currentBlobs) {
            b.id = blobCounter;
            blobs.add(b);
            blobCounter++;
        }
    } else if (blobs.size() <= currentBlobs.size()) {
        // Match whatever blobs you can match
        for (Blob b : blobs) {
            float recordD = 1000;
            Blob matched = null;
            for (Blob cb : currentBlobs) {
                PVector centerB = b.getCenter();
                PVector centerCB = cb.getCenter();         
                float d = PVector.dist(centerB, centerCB);
                if (d < recordD && !cb.taken) {
                    recordD = d; 
                    matched = cb;
                }
            }
            matched.taken = true;
            b.become(matched);
        }

        // Whatever is leftover make new blobs
        for (Blob b : currentBlobs) {
            if (!b.taken) {
                b.id = blobCounter;
                blobs.add(b);
                blobCounter++;
            }
        }
    } else if (blobs.size() > currentBlobs.size()) {
        for (Blob b : blobs) {
            b.taken = false;
        }


        // Match whatever blobs you can match
        for (Blob cb : currentBlobs) {
            float recordD = 1000;
            Blob matched = null;
            for (Blob b : blobs) {
                PVector centerB = b.getCenter();
                PVector centerCB = cb.getCenter();         
                float d = PVector.dist(centerB, centerCB);
                if (d < recordD && !b.taken) {
                    recordD = d; 
                    matched = b;
                }
            }
            if (matched != null) {
                matched.taken = true;
                // Resetting the lifespan here is no longer necessary since setting `lifespan = maxLife;` in the become() method in Blob.pde
                // matched.lifespan = maxLife;
                matched.become(cb);
            }
        }

        for (int i = blobs.size() - 1; i >= 0; i--) {
            Blob b = blobs.get(i);
            if (!b.taken) {
                if (b.checkLife()) {
                    blobs.remove(i);
                }
            }
        }
    }

    for (Blob b : blobs) {
        b.show();
    } 
}
        