import React, { Component } from 'react';
import { Container } from 'reactstrap';
import Footer from '../Footer';


export class UserLayout extends Component {
    static displayName = UserLayout.name;

    render() {
        return (
            <div>
                <Container>
                    <h1>User Layout</h1>
                </Container>
                <Footer />
            </div>
        );
    }
}
