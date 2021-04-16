import React, { Component } from 'react';
import MainSection from './components/MainSection';
const mainFeaturedPost = {
    title: 'Title of a longer featured blog post',
    description:
        "Multiple lines of text that form the lede, informing new readers quickly and efficiently about what's most interesting in this post's contents.",
    image: 'https://source.unsplash.com/random',
    imgText: 'main image description',
    linkText: 'Continue reading…',
};


class Home extends Component {
    state = {}

    render() {
        return (
            <div>
                <MainSection post={mainFeaturedPost} />

            </div>
        );
    }
}

export default Home;