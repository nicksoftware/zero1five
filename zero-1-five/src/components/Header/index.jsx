import React from 'react';
import PropTypes from 'prop-types';
import { makeStyles } from '@material-ui/core/styles';
import Toolbar from '@material-ui/core/Toolbar';

import Link from '@material-ui/core/Link';
import FrontTopMenu from '../Menu/NavMenu';
const useStyles = makeStyles((theme) => ({
    toolbar: {
        borderBottom: `1px solid ${theme.palette.divider}`,
    },
    toolbarTitle: {
        flex: 1,
    },
    toolbarSecondary: {
        justifyContent: 'space-between',
        overflowX: 'auto',
        borderBottom: `1px solid ${theme.palette.divider}`,
        marginBottom: '20px'
    },
    toolbarLink: {
        padding: theme.spacing(1),
        flexShrink: 0,
    },
}));
const Header = (props) => {
    const classes = useStyles();
    const { categories, title } = props;

    return (
        <React.Fragment>
            <FrontTopMenu title={title} />
            <Toolbar component="nav" variant="dense" className={classes.toolbarSecondary}>
                {categories.map((section) => (
                    <Link
                        color="inherit"
                        noWrap
                        key={section.title}
                        variant="body2"
                        href={section.url}
                        className={classes.toolbarLink}
                    >
                        {section.title}
                    </Link>
                ))}
            </Toolbar>
        </React.Fragment>
    );
}
export default Header;

Header.propTypes = {
    sections: PropTypes.array,
    title: PropTypes.string,
};