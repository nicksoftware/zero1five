import React from 'react';
import Typography from '@material-ui/core/Typography';
import Container from '@material-ui/core/Container';
import Copyright from './Copyright';
import * as styles from '../../Styles';

const Footer = () => {
    const classes = styles.useStyles();
    return (
        <div className={classes.root}>
            <footer className={classes.footer}>
                <Container maxWidth="sm">
                    <Typography variant="body1">This is the Footer</Typography>
                    <Copyright />
                </Container>
            </footer>
        </div>
    );
}


export default Footer