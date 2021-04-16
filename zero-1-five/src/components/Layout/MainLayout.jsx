import React, { Component } from 'react';
import { Container } from 'reactstrap';
import DocumentTitle from 'react-document-title';
import { Redirect, Route, Switch } from 'react-router';
import { frontRouters } from '../Router/router.config';
import Footer from '../Footer';
import Header from '../Header';


export class MainLayout extends Component {
  constructor(props) {
    super(props);
    this.categories = [
      { title: 'Technology', url: '#' },
      { title: 'Design', url: '#' },
      { title: 'Culture', url: '#' },
      { title: 'Business', url: '#' },
      { title: 'Politics', url: '#' },
      { title: 'Opinion', url: '#' },
      { title: 'Science', url: '#' },
      { title: 'Health', url: '#' },
      { title: 'Style', url: '#' },
      { title: 'Travel', url: '#' },
    ];
  }
  static displayName = MainLayout.name;

  render() {

    const layout = (
      <div>
        <Header categories={this.categories} title="Zero 1 Five" />
        <Container>
          <Switch>
            {/* <Redirect path='/' to='/front' /> */}
            {
              frontRouters.filter(it => !it.isLayout)
                .map((route, index) => (
                  <Route exact key={index}
                    path={route.path} component={route.component}
                    render={(props) => <Route component={route.component} permission={route.permission} />}
                  />
                ))
            }
            <Redirect to="/notfound" />
          </Switch>
        </Container>
        <Footer />
      </div>
    );
    return (<DocumentTitle title="" >{layout}</DocumentTitle>);
  }


}
