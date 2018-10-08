<template>
    <v-dialog v-model="showProfile" fullscreen transition="slide-y-transition">
        <v-btn slot="activator" flat fab>
            <v-icon>fas fa-user</v-icon>
        </v-btn>
        <v-layout>
            <v-toolbar color="amber darken-4">
                <v-spacer></v-spacer>
                <v-btn @click="showProfile = false" fab flat>
                    <v-icon>close</v-icon>
                </v-btn>
            </v-toolbar>
        </v-layout>
    </v-dialog>
</template>
<script>
export default {
    name: 'profile',
    data() {
        return {
            showProfile: false
        }
    }
}
</script>
