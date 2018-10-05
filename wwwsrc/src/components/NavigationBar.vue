<template>
    <v-toolbar>
        <v-btn flat href="#"> <!-- route to top of page -->
            <v-icon color="primary" large>fab fa-korvue</v-icon>
        </v-btn>
        <v-toolbar-items>
            <v-text-field prepend-inner-icon="search" single-line clearable v-model="search"></v-text-field>
        </v-toolbar-items>
        <v-toolbar-title>keeper</v-toolbar-title>
        <v-spacer></v-spacer>
        <v-toolbar-items>
            <v-btn>
                new keep
            </v-btn>
            <v-btn>
                my vaults
            </v-btn>
            <v-btn icon>
                <v-icon>fas fa-user</v-icon>
            </v-btn>
            <v-btn icon @click="logout()">
                <v-icon>fa-sign-out-alt</v-icon>
            </v-btn>
        </v-toolbar-items>
    </v-toolbar>    
</template>
<script>
export default {
    name: "navigationBar",
    data(){
        return {
            search: ""
        }
    },
    methods: {
        logout()
        {
            this.$store.dispatch("logout")
        }
    }
}
</script>
<style>

</style>