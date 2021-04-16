import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import Button from '@material-ui/core/Button';
import IconButton from '@material-ui/core/IconButton';
import SearchIcon from '@material-ui/icons/Search'
import './NavMenu.css';
import Logo from '../../assets/logo.png';

class FrontTopMenu extends Component {
  static displayName = FrontTopMenu.name;
  constructor(props) {
    super(props);

    this.toggleNavbar = this.toggleNavbar.bind(this);
    this.state = {
      collapsed: true
    };
  }
  toggleNavbar() {
    this.setState({
      collapsed: !this.state.collapsed
    });
  }

  render() {

    return (
      <header>
        <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3"

          light>
          <Container  >
            <NavbarBrand tag={Link} to="/">
              <img src={Logo} alt='logo' />
            </NavbarBrand>
            <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
            <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!this.state.collapsed} navbar>
              <ul className="navbar-nav flex-grow">
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/front">Home</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/about">About</NavLink>
                </NavItem>
                <IconButton>
                  <SearchIcon />
                </IconButton>
                <Button variant="outlined" size="small">
                  Sign up
                </Button>
              </ul>
            </Collapse>
          </Container>
        </Navbar>
      </header>
    );
  }
}
export default FrontTopMenu;