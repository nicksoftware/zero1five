import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from '../Menu/NavMenu';

export class MainLayout extends Component {
  static displayName = MainLayout.name;

  render() {
    return (
      <div>
        <NavMenu />
        <Container>
          {this.props.children}
        </Container>
      </div>
    );
  }
}
